using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : NetworkBehaviour
{
    private Animator animator;
    public AudioSource audioSrc;

    public uint playerID;

    public bool hasCollided = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            animator = GetComponent<Animator>();
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<vThirdPersonCamera>().target = this.gameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
            audioSrc.PlayDelayed(0.8f);
        }

        if (hasCollided)
        {
            UpdateLocalScore();
            hasCollided = false;
        }
    }

    void UpdateLocalScore()
    {
        Text hitCounter = GameObject.Find("Canvas/HitCounter").GetComponent<Text>();
        int count = Int32.Parse(hitCounter.text);
        count++;
        hitCounter.text = count.ToString();
    }
}
