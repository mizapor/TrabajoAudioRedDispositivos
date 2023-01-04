using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SphereSideMovement : MonoBehaviour
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

            Text hitCounter = GameObject.Find("Canvas/HitCounter").GetComponent<Text>();
            int count = Int32.Parse(hitCounter.text);
            count++;
            hitCounter.text = count.ToString();
            Destroy(this.gameObject);
        }
    }
}
