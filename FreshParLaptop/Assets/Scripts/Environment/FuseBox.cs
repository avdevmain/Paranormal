using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FuseBox : NetworkBehaviour
{
    [SyncVar (hook= nameof(OnToggle))]
    public bool state = true;
    
   
    LightSource[] lights;

    void Start()
    {
       lights =  FindObjectsOfType<LightSource>();
       Debug.Log(lights.Length);
    }

    void OnToggle(bool oldValue, bool newValue)
    {
        if (newValue)
        {
            foreach(LightSource light in lights)
            {
                light.interactable = true;
                light.transform.GetChild(0).gameObject.SetActive(light.state);
            }
        }
        else
        {
            foreach(LightSource light in lights)
            {
                light.interactable = false;
                light.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
