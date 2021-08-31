using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class PhysicalObject : NetworkBehaviour
{
    [ClientRpc]
    public void RpcThrow(Vector3 force)
    {
        GetComponent<Rigidbody>().AddForce(force);
    }
}
