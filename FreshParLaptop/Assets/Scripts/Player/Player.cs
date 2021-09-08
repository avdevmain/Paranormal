using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Player : NetworkBehaviour
{

    public PlayerHealth health;
    public bool IsLeader = false;
    public bool isAlive;

    public float sanityLevel = 100;
  
    public int currentRoom;

    private MyNetworkManager net_manager;
    private MyNetworkManager Net_manager
    {
        get
        {
            if (net_manager != null) { return net_manager; }
            return net_manager = NetworkManager.singleton as MyNetworkManager;
        }
    }    
    
    public override void OnStartClient()
    {
        base.OnStartClient();
        Net_manager.Players.Add(this);
    }

    public override void OnStopClient()
    {
        Net_manager.Players.Remove(this);
        base.OnStopClient();
    }

    void Start() {
       SetupPlayer();

    }

    void Update() {
        if (IsLeader) //Debug штуки
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                CmdDebugCreateGhost();
            }

            if (Input.GetKey(KeyCode.L))
            {
                CmdDebugEnemy();
            }
        }
    }

    [Command]
    void CmdDebugCreateGhost()
    {
        Net_manager.levelManager.CreateEnemy();
    }

    [Command]
    void CmdDebugEnemy()
    {
       // Net_manager.levelManager.enemy.enemyActivity.Interact();
    }


    public void SetupPlayer()
    {
        if (!hasAuthority)
        {   transform.GetChild(1).gameObject.SetActive(false); //GUI игрока
            transform.GetChild(2).gameObject.SetActive(false); //Камера игрока
            GetComponent<PlayerController>().enabled = false;
            GetComponent<PlayerMotor>().enabled = false;
            GetComponent<ItemDatabase>().enabled = false;
            GetComponent<Interaction>().enabled = false;
        }
        health = GetComponent<PlayerHealth>();
    }


   
    private void OnTriggerEnter(Collider other) {
        if (!isLocalPlayer) return;
        if (other.tag == "Room")
        {currentRoom = int.Parse(other.name);
        CmdEnteredZone(currentRoom);}
        
    }

    [Command]
    public void CmdEnteredZone(int roomNum)
    {
        currentRoom = roomNum;
        if (roomNum > 0)
        {
            if (!Net_manager.levelManager.countdownStarted)
            {
                Net_manager.levelManager.countdownStarted = true;
                RpcStartCountdown();}

        }
    }

    [ClientRpc]
    public void RpcStartCountdown()
    {
        Net_manager.levelManager.timer.StartCoundown();
    }



}
