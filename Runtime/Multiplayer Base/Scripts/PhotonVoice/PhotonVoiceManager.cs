using Photon.Voice.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonVoiceManager : MonoBehaviour
{
    [SerializeField]
    private Recorder recorder;

    private void Awake()
    {
        recorder.TransmitEnabled = true;
        recorder.VoiceDetection = true;
    }

}
