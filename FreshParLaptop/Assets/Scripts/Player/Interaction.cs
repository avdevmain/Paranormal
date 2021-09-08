using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Interaction : NetworkBehaviour
{
    public Camera playerCam;
    public float rayRange = 3f;
    public KeyCode pickupKey = KeyCode.E;
    public KeyCode useKey = KeyCode.F;
    public Text toolTip;

    private void Start() {
        if (!hasAuthority)
        {
            playerCam.enabled = false;
        }
        else
        {
            toolTip.text = "";
        }
    }

    private void Update()
    {
        if (!hasAuthority) return;
        LookForObject();

    }

    void LookForObject()
    {
        Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = 1 << 8;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, rayRange, layerMask))
        {
            GameObject objHit = hit.collider.gameObject;
            if (objHit.GetComponent<ScriptableItem>())
            {
                if (toolTip.text=="")
                    toolTip.text = "E to pickup " + objHit.GetComponent<ScriptableItem>().title;

                if ((Input.GetKeyDown(pickupKey)) && (GetComponent<PlayerEquip>().HasFreeSpace().HasValue))
                {
                    
                    //NetworkClient.connection.identity.GetComponent<PlayerEquip>().CmdPickupItem(objHit.transform.parent.gameObject);
                    GetComponent<PlayerEquip>().CmdPickupItem(objHit.transform.parent.gameObject, GetComponent<NetworkIdentity>());
                }
                if (Input.GetKeyDown(useKey))
                {
                    Debug.Log(objHit.transform.parent.name);
                    CmdToggleSceneItem(objHit.transform.parent.gameObject);
                }
            }
            else
            if (objHit.GetComponent<LightSource>())
            {
                if (toolTip.text=="")
                    toolTip.text = "F to toggle light";

                if (Input.GetKeyDown(useKey))
                {
                    CmdUseLight(objHit);
                }
            }
            else
            if (objHit.GetComponent<FuseBox>())
            {   
                if (toolTip.text=="")
                    toolTip.text = "F to use fuse box";

                if (Input.GetKeyDown(useKey))
                {
                    CmdUseFuseBox(objHit);
                }
            }
            else
            if (objHit.GetComponent<AudioObject>())
            {
                if (toolTip.text=="")
                    toolTip.text = "F to use audio obj";

                if (Input.GetKeyDown(useKey))
                {
                    CmdUseAudioObj(objHit);
                }
            }
            else
            if (objHit.GetComponent<PhysicalObject>())
            {
                if (toolTip.text=="")
                    toolTip.text = "F to throw phys obj";

                if (Input.GetKeyDown(useKey))
                {
                    CmdUsePhysicObj(objHit);
                }
            }
            else
            if (objHit.GetComponent<Window>())
            {
                if (toolTip.text=="")
                    toolTip.text = "F to use window";

                if (Input.GetKeyDown(useKey))
                {
                    CmdUseWindow(objHit);
                }
            }
            else
            if (objHit.GetComponent<Door>())
            {
                if (toolTip.text=="")
                    toolTip.text = "F to use door";

                if (Input.GetKeyDown(useKey))
                {
                    CmdUseDoor(objHit);
                }
            }
        }
        else
        {
            toolTip.text = "";  //Обнуление текста надо будет доделывать. Сейчас при моментальном переведении курсора с одного обьекта на другой тултип не изменится.
        }
    }

    [Command]
    void CmdToggleSceneItem(GameObject obj)
    {
        SceneObject sceneObj = obj.GetComponent<SceneObject>();
        sceneObj.equippedItemState = !sceneObj.equippedItemState;
    }
   
   [Command]
   void CmdUseLight(GameObject switchObj)
   {
       if (switchObj.GetComponent<LightSource>().interactable)
        switchObj.GetComponent<LightSource>().state = !switchObj.GetComponent<LightSource>().state;
   }

   [Command]
   void CmdUseFuseBox(GameObject fuseObj)
   {
       fuseObj.GetComponent<FuseBox>().state = !fuseObj.GetComponent<FuseBox>().state;
   }

   [Command]
   void CmdUseAudioObj(GameObject intObj)
   {
       intObj.GetComponent<AudioObject>().RpcUse();
   }
   [Command]
   void CmdUsePhysicObj(GameObject physObj)
   {
       //physObj.GetComponent<PhysicalObject>().RpcThrow(transform.forward * -300f);
   }
   [Command]
   void CmdUseDoor(GameObject doorObj)
   {
       doorObj.GetComponent<Door>().OpenDoor(80f);
   }

   [Command]
   void CmdUseWindow(GameObject windowObj)
   {
       windowObj.GetComponent<Window>().OpenDoor(80f);
   }
}
