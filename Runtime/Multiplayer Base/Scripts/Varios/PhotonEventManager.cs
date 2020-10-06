using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
public class PhotonEventManager : MonoBehaviour
{
    public Action OnInstanceMyAvatar;
    public Action<bool> OnReceiveTeleportStatus, OnReceiveMuteStatus, OnReceiveControlStatus;
    public Action<int, string> OnReceiveJsonInformation;
    public static PhotonEventManager Instance;
    private bool muteAll, blockTpAll;

    public bool MuteAll { get => muteAll; set => muteAll = value; }
    public bool BlockTpAll { get => blockTpAll; set => blockTpAll = value; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }
    [PunRPC]
    public void ReceiveTeleportStatus(int idUser, bool status)
    {
        PlayerInformation playerInformation;
        OnlineUserInformation.Instance.Players.TryGetValue(idUser, out playerInformation);
        if (playerInformation != null)
            playerInformation.IsBLockedTp = status;
        if (OnlineUserInformation.Instance.ReturnMyPhotonId() == idUser)
        {
            OnReceiveTeleportStatus?.Invoke(status);
        }
        StartCoroutine(WaitForIteration(playerInformation));
    }
    [PunRPC]
    public void ReceiveMuteStatus(int idUser, bool status)
    {
        PlayerInformation playerInformation;
        OnlineUserInformation.Instance.Players.TryGetValue(idUser, out playerInformation);
        if (playerInformation != null)
            playerInformation.IsMuted = status;
        if (OnlineUserInformation.Instance.ReturnMyPhotonId() == idUser)
        {
            Debug.Log("Me llego a mi MUTE en modo: " + status);
            OnReceiveMuteStatus?.Invoke(status);
        }
        StartCoroutine(WaitForIteration(playerInformation));
    }
    [PunRPC]
    public void ReceiveControlStatus(int idUser)
    {
        PlayerInformation playerInformation;
        OnlineUserInformation.Instance.Players.TryGetValue(idUser, out playerInformation);
        if (playerInformation != null)
            playerInformation.IsControlling = true;
        if (OnlineUserInformation.Instance.ReturnMyPhotonId() == idUser)
        {
            Debug.Log("Me llego a mi Contol en modo: " + true);
            OnReceiveControlStatus?.Invoke(true);
        }
        else
        {
            Debug.Log("Me llego a mi Contol en modo: " + false);
            OnReceiveControlStatus?.Invoke(false);
        }
        StartCoroutine(WaitForIterationControl(playerInformation));
    }
    [PunRPC]
    public void ReceiveJsonInformation (int idUSer, int idInfo, string Json)
    {
        if (OnlineUserInformation.Instance.ReturnMyPhotonId() == idUSer)
        {
            OnReceiveJsonInformation?.Invoke(idInfo, Json);
        }
    }
    [PunRPC]
    public void Disconnet()
    {
        Debug.Log("disconected AND closed");
        PhotonNetwork.Disconnect();
        PhotonNetwork.DestroyAll(true);
    }
    public void OnNewPlayer(string UserId)
    {
        StartCoroutine(WaitForPlayer(UserId));
    }
    private IEnumerator WaitForPlayer(string id)
    {
        yield return new WaitUntil(() => OnlineUserInformation.Instance.ReturnPlayerByPhotonUserId(id) != null);
        var player = OnlineUserInformation.Instance.ReturnPlayerByPhotonUserId(id);
        if (blockTpAll)
            ControlEventsUsers.Instance.BlockTeleportToSpecificUser(player.Id,true);
        if (muteAll)
            ControlEventsUsers.Instance.MuteSpecificUser(player.Id,true);
        ControlEventsUsers.Instance.SendJsonInformation(player.Id, JsonConvert.SerializeObject(OnlineUserInformation.Instance.Players));
    }

    private IEnumerator WaitForIteration(PlayerInformation playerInformation)
    {
        yield return new WaitUntil(() => !OnlineUserInformation.Instance.IsIteratingPlayers);
        OnlineUserInformation.Instance.EditPlayer(playerInformation);
    }
    private IEnumerator WaitForIterationControl(PlayerInformation playerInformation)
    {
        yield return new WaitUntil(() => !OnlineUserInformation.Instance.IsIteratingPlayers);
        OnlineUserInformation.Instance.EditPlayerControl(playerInformation);
    }
}
