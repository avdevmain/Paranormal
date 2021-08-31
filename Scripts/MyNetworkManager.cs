using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    public List<Player> Players { get; } = new List<Player>();
    public LevelManager levelManager;

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            if (SceneManager.GetActiveScene().name == "Test")
            {
                bool isLeader = Players.Count == 0;

                 Transform startPos = GetStartPosition();
                GameObject PlayerInstance = startPos != null
                ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
                : Instantiate(playerPrefab);

                PlayerInstance.GetComponent<Player>().IsLeader = isLeader;
               // PlayerInstance.GetComponent<Player>().levelManager = levelManager;
                NetworkServer.AddPlayerForConnection(conn, PlayerInstance);
            }
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            if (conn.identity != null)
            {
                var player = conn.identity.GetComponent<Player>();

                Players.Remove(player);

            }

            base.OnServerDisconnect(conn);
        }

        public override void OnStopServer()
        {
            base.OnStopServer();
            Players.Clear();
        }
}
