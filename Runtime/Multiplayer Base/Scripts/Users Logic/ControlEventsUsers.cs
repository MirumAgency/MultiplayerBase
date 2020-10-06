using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControlEventsUsers : MonoBehaviour
{
    [SerializeField]
    private GameObject ControlObject;
    [SerializeField]
    private PhotonView photonView;
    [SerializeField]
    private UnityEvent OnEnableControlObject;

    public static ControlEventsUsers Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        if (PhotonNetwork.IsMasterClient)
        {
            if (ControlObject)
            {
                ControlObject.SetActive(true);
                OnEnableControlObject.Invoke();
            }
        }
    }
    public void BlockTeleportAllPlayers(bool status)
    {
        var listUsers = OnlineUserInformation.Instance.Players;
        PhotonEventManager.Instance.BlockTpAll = status;
        foreach (var user in listUsers)
        {
            Debug.Log("Bloqueando TP a: " + user.Key + status);
            BlockTeleportToSpecificUser(user.Key, status);
        }
    }
    public void DisconnectMaster()
    {
        photonView.RPC("Disconnet", RpcTarget.All);
    }
    public void MuteAllPlayers(bool status)
    {
        var listUsers = OnlineUserInformation.Instance.Players;
        PhotonEventManager.Instance.MuteAll = status;
        foreach (var user in listUsers)
        {
            Debug.Log("Mutenado a: " + user.Key);
            MuteSpecificUser(user.Key, status);
        }
    }
    [EasyButtons.Button]
    public void BlockAllTp()
    {
        BlockTeleportAllPlayers(true);
    }
    [EasyButtons.Button]
    public void MuteAll()
    {
        MuteAllPlayers(true);
    }
    [EasyButtons.Button]
    public void UnMuteAll()
    {
        MuteAllPlayers(false);
    }
    [EasyButtons.Button]
    public void UnBlockAllTp()
    {
        BlockTeleportAllPlayers(false);
    }
    [EasyButtons.Button]
    public void BlockTpToPLayer2()
    {
        BlockTeleportToSpecificUser(2001, true);
    }
    [EasyButtons.Button]
    public void UnBlockTpToPLayer2()
    {
        BlockTeleportToSpecificUser(2001, false);
    }
    public void BlockTeleportToSpecificUser(int idUser, bool status)
    {
        photonView.RPC("ReceiveTeleportStatus", RpcTarget.All, idUser, status);
    }
    [EasyButtons.Button]
    public void MutePlayerTwo()
    {
        MuteSpecificUser(2001, true);
    }
    [EasyButtons.Button]
    public void UnMutePlayerTwo()
    {
        MuteSpecificUser(2001, false);
    }
    public void MuteSpecificUser(int idUser, bool status)
    {
        photonView.RPC("ReceiveMuteStatus", RpcTarget.All, idUser, status);
    }
    public void SendJsonInformation(int idUser, string Json)
    {
        photonView.RPC("ReceiveJsonInformation", RpcTarget.All, idUser, (int)JsonIds.UpdatePlayersFromMaster, Json);
    }
    [EasyButtons.Button]
    public void ControlToPlayerTwo()
    {
        ControlToSpecificUser(2001);
    }
    [EasyButtons.Button]
    public void ControlToMaster()
    {
        ControlToSpecificUser(1001);
    }
    public void ControlToSpecificUser(int idUser)
    {
        photonView.RPC("ReceiveControlStatus", RpcTarget.All, idUser);
    }
}
