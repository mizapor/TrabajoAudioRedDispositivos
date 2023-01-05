using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSideMovement : NetworkBehaviour
{
    public Transform farInit;
    public Transform farEnd;
    private Vector3 initGoal;
    private Vector3 endGoal;
    public float secondsForOneLength; // Lower the value, faster the sphere moves

    void Awake()
    {
        initGoal = farInit.position;
        endGoal = farEnd.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(initGoal, endGoal, Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.time / secondsForOneLength, 1f)));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "sword")
        {
            Debug.Log("Collided with :" + other.tag);
            GameObject.Find("NetworkManager").GetComponent<SphereManager>().SphereHitCallbackCommand(this.gameObject);
        }
    }
}
