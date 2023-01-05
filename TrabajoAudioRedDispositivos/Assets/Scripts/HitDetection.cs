using Mirror;
using System;
using System.Linq;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    //private bool canHitSphere = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sphere"/* && canHitSphere*/)
        {
            //canHitSphere = false;
            Debug.Log("Collided with :" + other.tag);

            this.transform.root.GetComponent<Attack>().hasCollided = true;
        }
    }
}
