using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : NetworkBehaviour
{
    private Animator animator;
    public AudioSource audioSrc;
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
    }
}
