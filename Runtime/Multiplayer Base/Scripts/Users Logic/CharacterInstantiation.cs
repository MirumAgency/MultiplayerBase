using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterInstantiation : MonoBehaviourPunCallbacks, IOnEventCallback
{
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
            InstantiateCharacter();
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("He entrado en el Room");
        InstantiateCharacter();
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"el player {newPlayer.NickName} ha entrado al room");
        PhotonEventManager.Instance.OnNewPlayer(newPlayer.UserId);
        base.OnPlayerEnteredRoom(newPlayer);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("leftroom "+otherPlayer.UserId);
        OnlineUserInformation.Instance.ShowPlayers();
        

        var player = OnlineUserInformation.Instance.ReturnPlayerByPhotonUserId(otherPlayer.UserId);
        if (player == null)
            return;
        if (player.IsControlling && !player.IsMaster)
            ControlEventsUsers.Instance.ControlToMaster();
        if (player.IsMaster)
            ControlEventsUsers.Instance.DisconnectMaster();
        if (player.PhotonView.IsMine)
            PhotonNetwork.DestroyPlayerObjects(otherPlayer);

        OnlineUserInformation.Instance.Players.Remove(player.Id);

    }

    public void InstantiateCharacter()
    {
        Debug.Log("Character");
        Transform parent = ParentAvatarsManager.Instance.AvatarClientsParent;
        var avatar = PhotonNetwork.Instantiate("DefaultAvatar", parent.position, Quaternion.identity);
        var tempPhotonView = avatar.GetComponent<PhotonView>();
        var colorGenerator = new ColorGenerator();
        var color = colorGenerator.ReturnRamdonGrayColor();
        avatar.GetComponent<AvatarReferences>().SetupInstantiation(tempPhotonView, color, DetectPlatform.Instance.DeviceDetected);
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.Others,
            CachingOption = EventCaching.AddToRoomCache
        };
        object[] data =
               {
                    tempPhotonView.ViewID, new Vector3(color.r,color.g,color.b), DetectPlatform.Instance.DeviceDetected
               };
        PhotonNetwork.RaiseEvent(10, data, raiseEventOptions, SendOptions.SendReliable);
    }
    public void OnEvent(EventData photonEvent)
    {
        object[] data = photonEvent.CustomData as object[];
        if (data != null)
        {
            if (photonEvent.Code == 10)
            {
                int ViewID = (int)data[0];
                Vector3 VectorColor = (Vector3)data[1];
                DevicesList device = (DevicesList)data[2];
                Color color = new Color(VectorColor.x, VectorColor.y, VectorColor.z);
                List<PhotonView> players = FindObjectsOfType<PhotonView>().ToList();
                PhotonView player = players.Find(x => x.ViewID == ViewID);
                player.GetComponent<AvatarReferences>().SetupInstantiation(player, color, device);
                Debug.Log("RaiseEvent "+ ViewID);
            }
        }
    }
}
