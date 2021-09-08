using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SceneObject : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnChangeEquipment))]
    public int equippedItemID = 0; 

    [SyncVar(hook = nameof(OnChangeState))]
    public bool equippedItemState;
 
    void OnChangeState(bool oldState, bool newState)
    {
        if(equippedItemID!=0)
        {
        transform.GetChild(0).GetComponent<ScriptableItem>().onUse.Invoke();
        }
    }
    void OnChangeEquipment(int oldEquippedItemID, int newEquippedItemID)
    {
        StartCoroutine(ChangeEquipment(newEquippedItemID));
    }

    // Since Destroy is delayed to the end of the current frame, we use a coroutine
    // to clear out any child objects before instantiating the new one
    IEnumerator ChangeEquipment(int newEquippedItemID)
    {
        while (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
            yield return null;
        }

        // Use the new value, not the SyncVar property value
        SetEquippedItem(newEquippedItemID);
    }

    // SetEquippedItem is called on the client from OnChangeEquipment (above),
    // and on the server from CmdDropItem in the PlayerEquip script.
    public void SetEquippedItem(int newEquippedItemID)
    {
        GameObject newObj = Instantiate(ItemDatabase.FindItem(newEquippedItemID), transform);

        if (equippedItemState)
        newObj.GetComponent<ScriptableItem>().onUse.Invoke();
        //if (state)
            // newObj.GetComponent<ScriptableItem>().onUse.Invoke();       
        
    }

}
