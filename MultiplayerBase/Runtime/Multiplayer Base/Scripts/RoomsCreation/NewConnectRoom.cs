using UnityEngine;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine.Events;
using Photon.Pun;
using Photon;
public class NewConnectRoom : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    [SerializeField]
    private bool connectAutomatic = true, DivideRoomsByVersions, SyncScenesInClients = false;
    [SerializeField]
    private string titleRooms;
    [SerializeField]
    private Transform containerList;
    [SerializeField]
    private PrefabRoomList prefabRoom;
    [SerializeField]
    private UnityEvent Onclient, onConnectToMaster, OnJoinRoom;
    private int numRooms;
    private List<PrefabRoomList> roomLists;
    private List<RoomInfo> actualRooms;
    public void Start()
    {
        if (SyncScenesInClients)
            PhotonNetwork.AutomaticallySyncScene = true;
        else
            PhotonNetwork.AutomaticallySyncScene = false;
        if (connectAutomatic)
            ConnectNow();
    }

    public void ConnectNow()
    {
        Debug.Log("triying connect to photon");
        if (DivideRoomsByVersions)
            PhotonNetwork.GameVersion = Application.version;
        else
            PhotonNetwork.GameVersion = 1.ToString();
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnClientButton()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
        Onclient.Invoke();
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        actualRooms = roomList;
        UpdateCachedRoomList();
    }
    private void UpdateCachedRoomList()
    {
        var roomList = actualRooms;
        numRooms = roomList.Count;

        foreach (Transform listComponent in containerList)
            Destroy(listComponent.gameObject);
        foreach (RoomInfo room in roomList)
        {
            PrefabRoomList listItem = Instantiate(prefabRoom, containerList.transform);
            listItem.nameText.text = room.Name;
        }
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server");
        onConnectToMaster.Invoke();
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public void CreateRoom()
    {
        RoomOptions options = new RoomOptions { MaxPlayers = 20, EmptyRoomTtl = 0, PublishUserId = true };

        PhotonNetwork.CreateRoom($"{titleRooms} {numRooms}", options, null);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        if (message.Contains("id already"))
        {
            numRooms++;
            CreateRoom();
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Se ingreso a un room, en este metodo, haz lo que necesitas con el Master");
        OnJoinRoom.Invoke();
    }
}