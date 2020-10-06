using UnityEngine;
using UnityEngine.Events;
public class ManageLocalMute : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnReceiveEventMute, OnReceiveEventUnMuted;
    private void Start()
    {
        PhotonEventManager.Instance.OnReceiveMuteStatus += ChangeLocalMuteByStatus;
    }
    public void ChangeLocalMuteByStatus(bool status)
    {
        Debug.Log("Cambiando mis settings en Mute por el status " + status);
        if (status)
            OnReceiveEventMute.Invoke();
        else
            OnReceiveEventUnMuted.Invoke();
    }
}
