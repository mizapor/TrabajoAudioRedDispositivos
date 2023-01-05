using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRmanager : MonoBehaviour
{

    [SerializeField]
    GameObject camera;

    bool isVR = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("V"))
        {
            if (isVR)
            {
                if (isLocalPlayer)
                {
                    camera.SetActive(true);
                    camera.GetComponent<vThirdPersonCamera>().target = this.gameObject.transform;
                }
            }
            else
            {
                camera.SetActive(false);
            }
        }
    }
}
