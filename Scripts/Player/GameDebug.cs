using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameDebug : NetworkBehaviour
{

    public GameObject objToSpawn;
    private GameObject spawnedObj;
    void Update()
    {
        if (!hasAuthority) return;

        if (isServer) //Только хост может прикалываться так (ну дебаг)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                CmdCreateItem();
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                if (spawnedObj!=null)
                CmdUnspawnItem();
            }
        }

    }
    
    [Command]
    void CmdCreateItem()
    {
        spawnedObj = Instantiate(objToSpawn);
        NetworkServer.Spawn(spawnedObj);
    }

    [Command]
    void CmdUnspawnItem()
    {
        NetworkServer.Destroy(spawnedObj);
        spawnedObj = null;
    }
  
}
