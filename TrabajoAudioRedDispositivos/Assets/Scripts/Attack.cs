using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : NetworkBehaviour
{
    private Animator animator;
    public AudioSource audioSrc;
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

        if (Input.GetKeyDown("V"))
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
                    vrCamera.GetComponent<Camera>().enabled = false;
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = true;
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<vThirdPersonCamera>().target = this.gameObject.transform;
                }
            }
            else
            {
                isVR = true;
                if (isLocalPlayer)
                {
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = false;
                    vrCamera.GetComponent<Camera>().enabled = true;
                    GameObject.FindGameObjectWithTag("XROrigin").transform = this.gameObject.transform;
                }
            }
        }
    }
}
