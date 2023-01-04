using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Webcam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WebCamTexture webcamTexture = new WebCamTexture();
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = webcamTexture;
        if (WebCamTexture.devices.Length > 0 && WebCamTexture.devices[0].name != "OBS Virtual Camera")
            webcamTexture.Play();
    }
}
