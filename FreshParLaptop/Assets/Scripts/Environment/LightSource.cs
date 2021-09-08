using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LightSource : NetworkBehaviour
{

    [SyncVar(hook=nameof(OnToggle))]
   public bool state = false;

    [SyncVar]
    public bool interactable = true;

    private AudioSource audioSource;

    [Client]
    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    [Client]
    void OnToggle(bool oldBool, bool newBool)
    {
        for (int i =0; i<transform.childCount;i++)
        {
            transform.GetChild(0).gameObject.SetActive(state);
            audioSource.Play();
        }
    }

}
