using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
public class LevelManager : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnLevelChanged))]
    public string levelID;
    
    public GameObject enemyPrefab;

    public Enemy enemy;

    public Transform levelInHierarchy;
    public Timer timer;

    [SyncVar]
    public bool countdownStarted = false;

    public DiffLevel difficulty;
    public enum DiffLevel
    {
        easy,
        normal,
        hard
    }



    private void Start() {
        DontDestroyOnLoad(this);
        timer = FindObjectOfType<Timer>();

        difficulty = DiffLevel.normal; //ВРЕМЕННО, потом выбор у игроков
    }
    void OnLevelChanged(string oldLvlID, string newLvlID)
    {
        //Change scene on every client
        if (SceneManager.GetActiveScene().name != newLvlID)
            {
               AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(newLvlID, LoadSceneMode.Single);
               StartCoroutine(AfterSceneLoaded(asyncLoad));
            }
    } 

    private IEnumerator AfterSceneLoaded(AsyncOperation operationToWait)
    {
       while(!operationToWait.isDone)
       {
           yield return null;
       }
        CreateEnemy();
    }

    [Server]
    public void CreateEnemy() //Debug
    {
        GameObject enemyObj = Instantiate(enemyPrefab);
        enemy = enemyObj.GetComponent<Enemy>();
        Transform roomsInBuilding = levelInHierarchy.GetChild(0).GetChild(0);
        int chosenRoom = Random.Range(1, roomsInBuilding.childCount); // 0 - это улица
        enemy.favouriteRoom = roomsInBuilding.GetChild(chosenRoom);
        enemyObj.transform.position = roomsInBuilding.GetChild(chosenRoom).transform.position;

        NetworkServer.Spawn(enemyObj);
    }

    [Server]
    public Transform GetRoom(int roomIndex)
    {
        Transform roomsInBuilding = levelInHierarchy.GetChild(0).GetChild(0);
        return roomsInBuilding.GetChild(roomIndex);
    }
    [Server]
    public void SetEnemyDest(Vector3 dest) // Debug
    {
       // if (enemy!=null)
        //enemy.enemyMovement.SetDestination(dest);
    }

    /*
    private IEnumerator NewLevel()
    {
        yield return new WaitForSeconds(5f);
     Debug.Log("Время вышло,  меняю сцену");
        levelID = "Test2"; 
    } */
}
