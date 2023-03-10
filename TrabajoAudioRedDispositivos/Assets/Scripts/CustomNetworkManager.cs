using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{ 
    public GameObject spherePrefab;
    private List<Transform> sphereInitPositions = new List<Transform>();

    private bool alreadyInit = false;

    public override void OnServerReady(NetworkConnectionToClient conn)
    {
        base.OnServerReady(conn);

        if (!alreadyInit)
        {
            var spawnPointsRoot = GameObject.Find("RandomPositionsSphere");


            for (int i = 0; i < spawnPointsRoot.transform.childCount; i++)
                sphereInitPositions.Add(spawnPointsRoot.transform.GetChild(i));

            var sphereGO = Instantiate(spherePrefab, sphereInitPositions[UnityEngine.Random.Range(0, sphereInitPositions.Count)].position, Quaternion.identity);

            NetworkServer.Spawn(sphereGO);

            alreadyInit= true;
        }
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        //base.OnServerAddPlayer(conn);
        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        //player.name += " " + numPlayers;

        //if (numPlayers == 0)
        //    player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        //else if (numPlayers == 1)
        //    player = Instantiate(player2Prefab, Vector3.zero, Quaternion.identity);

        player.GetComponent<Attack>().playerID = numPlayers;

        NetworkServer.AddPlayerForConnection(conn, player);
    }
}
