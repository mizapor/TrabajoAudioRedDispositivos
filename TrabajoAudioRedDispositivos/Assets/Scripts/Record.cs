using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Record : MonoBehaviour
{
    bool isRecording = false;
    public AudioSource audioSrc;
    private int numGrab = 0;

    const string playText = "PRESS 'M' TO START RECORDING";
    const string stopText = "PRESS 'M' AGAIN TO STOP RECORDING";
    private Text micInfo;
    private bool isInsideArea = false;

    List<float> tempRecording = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        micInfo = GameObject.Find("Canvas/MicrophoneInfoText").GetComponent<Text>();

        foreach (var device in Microphone.devices) 
            Debug.Log(device);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInsideArea)
        {
            if (!isRecording)
                micInfo.text = playText;
            else
                micInfo.text = stopText;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!isRecording)
            {
                audioSrc.clip = Microphone.Start(null, true, 10, 44100);
                isRecording = true;
                //Invoke("ResizeRecording", 10f);
            }
            else
            {
                if (isRecording)
                    micInfo.text = "";

                isRecording = false;
                Microphone.End(null);
                SavWav.Save("audio" + numGrab, audioSrc.clip);
                numGrab++;

                ////stop recording, get length, create a new array of samples
                //int length = Microphone.GetPosition(null);

                //Microphone.End(null);
                //float[] clipData = new float[length];
                //audioSrc.clip.GetData(clipData, 0);

                ////create a larger vector that will have enough space to hold our temporary
                ////recording, and the last section of the current recording
                //float[] fullClip = new float[clipData.Length + tempRecording.Count];
                //for (int i = 0; i < fullClip.Length; i++)
                //{
                //    //write data all recorded data to fullCLip vector
                //    if (i < tempRecording.Count)
                //        fullClip[i] = tempRecording[i];
                //    else
                //        fullClip[i] = clipData[i - tempRecording.Count];
                //}

                //audioSrc.clip = AudioClip.Create("Final AudioSource", fullClip.Length, 1, 44100, false);
                //audioSrc.clip.SetData(fullClip, 0);
                //SavWav.Save("audio" + numGrab, audioSrc.clip);
                //numGrab++;
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
            audioSrc.Play();
    }

    void ResizeRecording()
    {
        if (isRecording)
        {
            //add the next second of recorded audio to temp vector
            int length = 44100;
            float[] clipData = new float[length];
            audioSrc.clip.GetData(clipData, 0);
            tempRecording.AddRange(clipData);
            Invoke("ResizeRecording", 10f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isInsideArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isInsideArea = false;

            if (!isRecording)
                micInfo.text = "";
        }
    }
}
