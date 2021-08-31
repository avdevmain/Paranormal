using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using BehaviorDesigner.Runtime;

public class Enemy : NetworkBehaviour
{
    private MyNetworkManager net_manager;
    public MyNetworkManager Net_manager
    {
        get
        {
            if (net_manager != null) { return net_manager; }
            return net_manager = NetworkManager.singleton as MyNetworkManager;
        }
    }  
    public EnemyMovement enemyMovement;
    //public EnemyActivity enemyActivity;
    public EnemyTraits enemyTraits;
    public Animator animator;
    public GameObject sceneObjectPrefab;

    public Transform favouriteRoom;
    
    public int currentRoom;

    public bool canWander = true;
    public bool canHunt = true;
    public bool canAttack = true;
    public bool hasWalkedInSalt = false;
    public float activityMultiplier;
    public float bonusActivity;
    public float difficultyMultiplier; // 0.5, 1, 1.5 or 2


    private Material regularMat;
    private Material shadowMat;
    private Material huntMat;
    private SkinnedMeshRenderer skinRenderer;
    [Server]
    private void OnTriggerEnter(Collider other) {
        if (!isServer) return;
        if (other.tag == "Room")
            currentRoom = (int.Parse(other.name));
    }


    private void Start() {

        enemyMovement = GetComponent<EnemyMovement>();
        //enemyActivity = GetComponent<EnemyActivity>();
        enemyTraits = GetComponent<EnemyTraits>();

        if (!isServer)
        {
            enemyMovement.enabled = false;
            //enemyActivity.enabled = false;
        }
        else
        {
            GenerateNewEnemyTraits();

            animator = GetComponent<Animator>();
            GetComponent<BehaviorTree>().enabled = true;

            //regularMat = Resources.Load<Material>("Materials/Ghost/LowBody");
            //shadowMat = Resources.Load<Material>("Materials/Ghost/Shadow");
            //huntMat = Resources.Load<Material>("Materials/Ghost/GhostHunt");
            skinRenderer = transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>();
        }
    }

    [Server]
    private void GenerateNewEnemyTraits() //Потом сделать рандом :)
    {
        enemyTraits.enemyName = "Ivan Pupkin";
        enemyTraits.isShy = false;
        enemyTraits.deathLength = 50;
        enemyTraits.hasPeePee = true;
        enemyTraits.enemyType = EnemyTraits.EnemyType.Spirit;
    }


    [Server]
    public float GetAverageSanity()
    {
        float totalSanity = 0;
        int counter = 0;
        for (int i = 0; i<Net_manager.Players.Count;i++)
        {
            if (Net_manager.Players[i].isAlive)
            {
                counter++;
                totalSanity+=Net_manager.Players[i].sanityLevel;
            }
        }
        return totalSanity/counter;
    }
    [Server]
    public LevelManager.DiffLevel GetDiffLevel()
    {
        return Net_manager.levelManager.difficulty;
    }
    [Server]
    public void GetNearestPlayerDistance()
    {   
        float minDist  = 1000f;
        for (int i =0; i<Net_manager.Players.Count;i++)
        {
            float dist = Vector3.Distance(Net_manager.Players[i].transform.position, gameObject.transform.position);
            if (dist<minDist)
            {
                minDist = dist;
            }
        } 
    }

    [Server]
    public bool ArePlayersHere()
    {
        bool result = false;

        for (int i =0; i<Net_manager.Players.Count; i++)
        {
            if (Net_manager.Players[i].currentRoom == currentRoom)
                return true;
        }

        return result;
    }

    [Server]
    public bool HasTimerReachedZero()
    {
        return (Net_manager.levelManager.timer.SecondsLeft() == 0);
    }

    [Server]
    public bool WasDoorOpened()
    {
        return (Net_manager.levelManager.countdownStarted);
    }

    [Server]
    public BoxCollider GetRoomCollider(int roomIndex)
    {
        return Net_manager.levelManager.GetRoom(roomIndex).GetComponent<BoxCollider>();
    }
    [Server]
    public void SpawnObjOnPos(GameObject prefabToSpawn, Vector3 posToSpawn, AudioClip sound = null, bool emf = false)
    {
        GameObject obj = Instantiate(prefabToSpawn, posToSpawn, Quaternion.identity);
        EDFSource edf;
        if (emf)
        {
            if (obj.GetComponent<EDFSource>())
                edf = obj.GetComponent<EDFSource>();
            else
                Debug.LogError("No EDF source script on spawned object");
        }

        if (sound)
        {
            if (obj.GetComponent<AudioSource>())
            obj.GetComponent<AudioSource>().clip = sound;
            else
            Debug.LogError("No Audio Source script on spawned object");
        }
        
        NetworkServer.Spawn(obj);

    }

    [Server]
    public void AppearAtPos(Vector3 pos, Severity severity)
    {
        enemyMovement.WarpTo(pos);
        if (severity == Severity.easy)
        {
            RpcSetMaterial(shadowMat);
        }
        else
        if (severity == Severity.medium)
        {
            RpcSetMaterial(shadowMat);
        }
        else
        {
            RpcSetMaterial(shadowMat);
        }
        
        //+ с материалами и прочими RPC
    }

    [ClientRpc]
    private void RpcSetMaterial(Material whichMatToSet)
    {

    }

    [Server]
    private void Update()
    {

        if (activityMultiplier>0)
            activityMultiplier = Mathf.Clamp(activityMultiplier-Time.deltaTime/2, 0, 100);


    }



}
