using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour
{
    bool isRecording = false;
    public AudioSource audioSrc;
    private int numGrab = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var device in Microphone.devices)
        {
            Debug.Log(device);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!isRecording)
            {
                audioSrc.clip = Microphone.Start("Micrófono (2- USB Camera-B4.09.24.1)", true, 360, 44100);
                isRecording = true;
            }
            else
            {
                isRecording = false;
                Microphone.End("Micrófono (2- USB Camera-B4.09.24.1)");
                SavWav.Save("audio" + numGrab, audioSrc.clip);
                numGrab++;
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            audioSrc.Play();
        }
    }
}
