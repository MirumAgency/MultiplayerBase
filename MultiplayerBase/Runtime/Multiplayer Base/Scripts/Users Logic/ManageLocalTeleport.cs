using UnityEngine;
using UnityEngine.Events;

public class ManageLocalTeleport : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnReceiveEventBlock, OnReceiveEventUnBlock;
    private void Start()
    {
        PhotonEventManager.Instance.OnReceiveTeleportStatus += ChangeLocalTeleportStatus;
    }
    public void ChangeLocalTeleportStatus(bool status)
    {
        if (status)
            OnReceiveEventBlock.Invoke();
        else
            OnReceiveEventUnBlock.Invoke();
    }

    private void OnLevelWasLoaded(int level)
    {
        PhotonEventManager.Instance.OnReceiveTeleportStatus += ChangeLocalTeleportStatus;
    }
}
