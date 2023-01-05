using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SphereManager : NetworkBehaviour
{
    public GameObject spherePrefab;
    private List<Transform> sphereInitPositions = new List<Transform>();

    void Start()
    {
        var spawnPointsRoot = GameObject.Find("RandomPositionsSphere");

        
        for (int i = 0; i < spawnPointsRoot.transform.childCount; i++)
            sphereInitPositions.Add(spawnPointsRoot.transform.GetChild(i));
    }

    public void SphereHitCallbackCommand(GameObject sphereGO)
    {
        if (!isServer)
            return;

        NetworkServer.UnSpawn(sphereGO);
        NetworkServer.Destroy(sphereGO);
        Destroy(sphereGO);
        var sphere = Instantiate(spherePrefab, sphereInitPositions[UnityEngine.Random.Range(0, sphereInitPositions.Count)].position, Quaternion.identity);
        NetworkServer.Spawn(sphere);
    }
}
