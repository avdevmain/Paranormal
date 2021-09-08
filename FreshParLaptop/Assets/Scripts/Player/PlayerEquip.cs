using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class PlayerEquip : NetworkBehaviour
{
    public Animator animator;
    public int[] inventory;
    public int activeSlot;
    public GameObject sceneObjectPrefab;
    public GameObject rightHand;

    private IKControl iKControl;

    [SyncVar(hook = nameof(OnChangeEquipment))]
    public int equippedItemID; 

    [SyncVar(hook = nameof(OnChangeState))]
    public bool equippedItemState;

    [SyncVar]
    public bool pickingUp = false;

    public Transform rhand_target = null;
    public Transform relbow_target = null;
    //public Transform lhand_target = null;
    //public Transform lelbow_target = null;
    private void Start() {
        if (!hasAuthority) return;
        inventory = new int[4];
        activeSlot = 0;    
        iKControl = GetComponent<IKControl>();
        animator = GetComponent<Animator>();
        DontDestroyOnLoad(this);
    }

    public int? HasFreeSpace()
    {
        for (int i =0; i<inventory.Length; i++)
        {
            if (inventory[i]==0)
            {
                return i;
            }
        }

        return null;
    }

    [Command]
    void CmdToggleItemState()
    {
        equippedItemState = !equippedItemState;
    }
    void OnChangeState(bool oldState, bool newState)
    {
        if (equippedItemID==0 || pickingUp)
        {
            
        }
        else
            rightHand.transform.GetChild(0).GetComponent<ScriptableItem>().onUse.Invoke();
        
    }

    void OnChangeEquipment(int oldEquippedItemID, int newEquippedItemID)
    {
        if (oldEquippedItemID!=0)
            animator.SetBool(rightHand.transform.GetChild(0).GetComponent<ScriptableItem>().anim_name, false);
        StartCoroutine(ChangeEquipment(newEquippedItemID));
    }

    // Since Destroy is delayed to the end of the current frame, we use a coroutine
    // to clear out any child objects before instantiating the new one
    IEnumerator ChangeEquipment(int newEquippedItemID)
    {
        while (rightHand.transform.childCount > 0)
        {
            Destroy(rightHand.transform.GetChild(0).gameObject);
            yield return null;
        }
        if (newEquippedItemID!=0)
            {
                GameObject newObj = Instantiate(ItemDatabase.FindItem(newEquippedItemID).GetComponent<ScriptableItem>().equipPrefab, rightHand.transform);
                newObj.GetComponent<Collider>().enabled = false;
                ScriptableItem item = rightHand.transform.GetChild(0).GetComponent<ScriptableItem>();
                if (equippedItemState)
                    item.onUse.Invoke();
                if (isLocalPlayer)
                    item.SetLayer("InHands");  

                animator.SetBool(item.anim_name, true);
                //iKControl.rightHandTarget = rhand_target;
                //iKControl.rightElbowTarget = relbow_target;
                //iKControl.leftHandTarget = lhand_target;
               // iKControl.leftElbowTarget = lelbow_target;             
            }


    }

    [ClientCallback]
    void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            {CmdChangeEquippedItem(inventory[0]);activeSlot = 0;}
        if (Input.GetKeyDown(KeyCode.Alpha2)) //Убрал проверку на то, держит ли сейчас такой же предмет в  руках или нет (equippedItemID != id предмета в слоте)
            {CmdChangeEquippedItem(inventory[1]);activeSlot = 1;}
        if (Input.GetKeyDown(KeyCode.Alpha3))
            {CmdChangeEquippedItem(inventory[2]);activeSlot = 2;}
        if (Input.GetKeyDown(KeyCode.Alpha4))
            {CmdChangeEquippedItem(inventory[3]);activeSlot = 3;} 

        if (Input.GetKeyDown(KeyCode.I))
            CmdSpawnObject(1);

        if (Input.GetKeyDown(KeyCode.O))
            CmdSpawnObject(2);

        if (Input.GetKeyDown(KeyCode.P))
            CmdSpawnObject(3);

        if (Input.GetKeyDown(KeyCode.X) && equippedItemID != 0)
            CmdDropItem(GetComponent<NetworkIdentity>(), activeSlot);

        if (Input.GetMouseButtonDown(1))
        {
            if (inventory[activeSlot]!=0)
            {   
                CmdToggleItemState();
            }
        }
    }
 
    [Command]
    void CmdChangeEquippedItem(int selectedItemID)
    {
        equippedItemID = 0;
        equippedItemState = false;
        if (selectedItemID!=0)
            equippedItemID = selectedItemID;
        
    }

    [Command]
    void CmdDropItem(NetworkIdentity target, int slotIndex)
    {
        // Instantiate the scene object on the server
        Vector3 pos = rightHand.transform.position;
        Quaternion rot = rightHand.transform.rotation;
        GameObject newSceneObject = Instantiate(sceneObjectPrefab, pos, rot);
        inventory[activeSlot] = 0;
        // set the RigidBody as non-kinematic on the server only (isKinematic = true in prefab)
        newSceneObject.GetComponent<Rigidbody>().isKinematic = false;

        newSceneObject.GetComponent<Rigidbody>().AddForce(transform.forward * 60f);
        
        SceneObject sceneObject = newSceneObject.GetComponent<SceneObject>();

        sceneObject.equippedItemState = equippedItemState;

        equippedItemState = false;

        // set the child object on the server
        sceneObject.SetEquippedItem(equippedItemID);

        // set the SyncVar on the scene object for clients
        sceneObject.equippedItemID = equippedItemID;

      ////  ScriptableItem oldItem = rightHand.transform.GetChild(0).GetComponent<ScriptableItem>();

        // set the player's SyncVar to nothing so clients will destroy the equipped child item
        equippedItemID = 0;

        

        TargetRmvFromInventory(target.connectionToClient, slotIndex);

        // Spawn the scene object on the network for all to see
        NetworkServer.Spawn(newSceneObject);
    }

    // CmdPickupItem is public because it's called from a script on the SceneObject
    [Command]
    public void CmdPickupItem(GameObject sceneObject, NetworkIdentity target)
    {
        SceneObject sceneObj = sceneObject.GetComponent<SceneObject>();
        // set the player's SyncVar so clients can show the equipped item
        
        pickingUp = true;

        equippedItemState = sceneObj.equippedItemState;

        equippedItemID = sceneObj.equippedItemID;
        
        TargetAddToInventory(target.connectionToClient, equippedItemID);

        // Destroy the scene object
        NetworkServer.Destroy(sceneObject);

        pickingUp = false;
        
    }

    [TargetRpc]
    public void TargetAddToInventory(NetworkConnection conn, int itemID)
    {
        int? freeSlot =  HasFreeSpace();

        inventory[(int)freeSlot] = itemID;
        activeSlot = (int)freeSlot;

    }

    [TargetRpc]
    public void TargetRmvFromInventory(NetworkConnection connection, int slotIndex)
    {
        inventory[slotIndex] = 0;
    }
    [Command]
    public void CmdSpawnObject(int id)
    {
        GameObject newSceneObject = Instantiate(sceneObjectPrefab);
        
        newSceneObject.GetComponent<Rigidbody>().isKinematic = false;

        SceneObject sceneObject = newSceneObject.GetComponent<SceneObject>();

        // set the child object on the server
        sceneObject.SetEquippedItem(id);

        // set the SyncVar on the scene object for clients
        sceneObject.equippedItemID = id;

        NetworkServer.Spawn(newSceneObject);

    }

}
