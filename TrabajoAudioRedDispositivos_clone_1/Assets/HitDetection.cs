using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public GameObject spherePrefab;
    public Transform[] sphereInitPositions;

    //private bool canHitSphere = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sphere"/* && canHitSphere*/)
        {
            //canHitSphere = false;
            Debug.Log("Collided with :" + other.tag);
            Instantiate(spherePrefab, sphereInitPositions[Random.Range(0, sphereInitPositions.Length)].position, Quaternion.identity);
        }
    }

    //void OnTriggerExit(Collider other)
    //{
    //    canHitSphere = true;
    //}
}
