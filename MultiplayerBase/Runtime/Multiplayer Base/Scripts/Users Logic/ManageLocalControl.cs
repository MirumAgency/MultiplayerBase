using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSourceMultiLanguage))]
public class ManageLocalControl : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnReceiveEventControl, OnReceiveEventRemoveControl;

    [SerializeField]
    private AudioSourceMultiLanguage audio;

    private void Reset()
    {
        audio = GetComponent<AudioSourceMultiLanguage>();
    }
    private void Start()
    {
        PhotonEventManager.Instance.OnReceiveControlStatus += ChangeLocalControlByStatus;
        audio.UpdateSpesificAudio();
    }
    public void ChangeLocalControlByStatus(bool status)
    {
        Debug.Log("Cambiando mis settings en CONTROL por el status " + status);
        if (status)
        {
            audio.PlayAudio();
            OnReceiveEventControl.Invoke();
        }
        else
            OnReceiveEventRemoveControl.Invoke();        
    }    
}
