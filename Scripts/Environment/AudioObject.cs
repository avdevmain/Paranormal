using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class AudioObject : NetworkBehaviour
{
    [ClientRpc]
    public void RpcUse()
    {
        GetComponent<AudioSource>().Play();
    }
}
