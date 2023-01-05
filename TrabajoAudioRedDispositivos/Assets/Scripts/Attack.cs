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
    bool isVR = false;

    GameObject vrCamera = null;

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

        if (Input.GetKeyDown(KeyCode.V))
        {
            foreach (Transform transform in GameObject.FindGameObjectWithTag("XROrigin").transform)
            {
                if (transform.CompareTag("VRCamera"))
                {
                    vrCamera = transform.gameObject;
                    break;
                }
            }
            if (isVR)
            {
                isVR = false;
                if (isLocalPlayer)
                {
                    GameObject.FindGameObjectWithTag("XROrigin").transform.parent = null;
                    vrCamera.GetComponent<Camera>().enabled = false;
                    vrCamera.GetComponent<AudioListener>().enabled = false;
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = true;
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioListener>().enabled = true;
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<vThirdPersonCamera>().target = this.gameObject.transform;
                }
            }
            else
            {
                isVR = true;
                if (isLocalPlayer)
                {
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = false;
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioListener>().enabled = false;
                    vrCamera.GetComponent<Camera>().enabled = true;
                    vrCamera.GetComponent<AudioListener>().enabled = true;
                    GameObject.FindGameObjectWithTag("XROrigin").transform.position = this.gameObject.transform.position;
                    GameObject.FindGameObjectWithTag("XROrigin").transform.rotation = this.gameObject.transform.rotation;
                    GameObject.FindGameObjectWithTag("XROrigin").transform.parent = this.gameObject.transform;
                    GameObject.FindGameObjectWithTag("XROrigin").transform.position = GameObject.FindGameObjectWithTag("XROrigin").transform.position + new Vector3(0, 1.7f, 0) + Vector3.forward * -0.5f;
                    
                }
            }
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
