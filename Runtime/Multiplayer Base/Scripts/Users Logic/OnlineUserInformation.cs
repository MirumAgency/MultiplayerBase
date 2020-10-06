using Newtonsoft.Json;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OnlineUserInformation : MonoBehaviour
{
    public static OnlineUserInformation Instance;
    [HideInInspector]
    public int ControllerPlayerId;
    public Action OnRefreshPlayerInfo;
    public Dictionary<int, PlayerInformation> Players = new Dictionary<int, PlayerInformation>();
    public PhotonView AvatarPhotonView { get => avatarPhotonView; set => avatarPhotonView = value; }
    public PlayerInformation LocalPlayerInformation { get => localPlayerInformation; set => localPlayerInformation = value; }
    public bool IsIteratingPlayers { get => isIteratingPlayers; set => isIteratingPlayers = value; }

    private PhotonView avatarPhotonView;
    private PlayerInformation localPlayerInformation;
    private bool isIteratingPlayers;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        ControllerPlayerId = 1001;
    }
    private void Start()
    {
        StartCoroutine(WaitForInstance());
    }
    public int ReturnMyPhotonId()
    {
        return localPlayerInformation.PhotonView.ViewID;
    }
    public PlayerInformation ReturnPlayerById(int id)
    {
        PlayerInformation p;
        if (Players.TryGetValue(id, out p))
            return p;
        else
            return null;
    }
    public PlayerInformation ReturnPlayerByPhotonUserId(string id)
    {
        PlayerInformation p;
        p = Players.FirstOrDefault(x => x.Value.UserId == id).Value;
        if (p != null)
            return p;
        else
            return null;
    }
    public PlayerInformation ReturnControllerPlayer()
    {
        PlayerInformation p;
        if (Players.TryGetValue(ControllerPlayerId, out p))
            return p;
        else
            return null;
    }
    public void AddPlayer(PlayerInformation player)
    {
        if (!Players.ContainsKey(player.Id))
        {
            Players.Add(player.Id, player);
        }
        ShowPlayers();
    }
    public void EditPlayer(PlayerInformation player)
    {
        isIteratingPlayers = true;
        if (Players.ContainsKey(player.Id))
        {
            Players[player.Id] = player;
        }
        OnRefreshPlayerInfo?.Invoke();
        ShowPlayers();
    }
    public void EditPlayerControl(PlayerInformation player)
    {
        isIteratingPlayers = true;
        var tempo = player;
        Players[player.Id] = player;
        foreach (var user in Players)
        {
            if (user.Key != player.Id)
                user.Value.IsControlling = false;
        }
        OnRefreshPlayerInfo?.Invoke();
        ShowPlayers();
    }
    public void UpdateListPlayersFromMaster (int idInfo, string Json)
    {
        if (idInfo == (int)JsonIds.UpdatePlayersFromMaster)
        {
            Debug.Log(Json);
            var MasterList = JsonConvert.DeserializeObject<Dictionary<int, PlayerInformation>>(Json);
            foreach(var player in Players)
            {
                player.Value.IsMuted = MasterList[player.Value.Id].IsMuted;
                player.Value.IsBLockedTp = MasterList[player.Value.Id].IsBLockedTp;
                player.Value.IsControlling = MasterList[player.Value.Id].IsControlling;
            }
            OnRefreshPlayerInfo?.Invoke();
        }
    }
    public void ShowPlayers()
    {
        isIteratingPlayers = true;
        foreach (var player in Players)
        {
            Debug.Log($"PLAYER: {player.Value.Name} {player.Value.Id} Ismute:{player.Value.IsMuted} IsBLockedTp:{player.Value.IsBLockedTp} Iscontrolling:{player.Value.IsControlling} photonID:{player.Value.UserId}");
        }
        isIteratingPlayers = false;
    }
    private IEnumerator WaitForInstance ()
    {
        yield return new WaitUntil(()=>PhotonEventManager.Instance != null);
        PhotonEventManager.Instance.OnReceiveJsonInformation += UpdateListPlayersFromMaster;
    }
}

[System.Serializable]
public class PlayerInformation
{
    public int Id;
    [JsonIgnore]
    public PhotonView PhotonView;
    public string Name;
    [JsonIgnore]
    public Color Color;
    public bool IsMaster;
    public bool IsControlling;
    [JsonIgnore]
    public DevicesList Device;
    [JsonIgnore]
    public Vector3 Position;
    [JsonIgnore]
    public Quaternion Rotation;
    public int? Answer;
    public bool IsMuted;
    public bool IsBLockedTp;
    [JsonIgnore]
    public Transform parent;
    public bool IsAFK = true;
    [JsonIgnore]
    public AvatarReferences avatarReferences;
    [JsonIgnore]
    public string UserId;

    public PlayerInformation(int id, PhotonView photonView, string name, Color color, bool isMaster, bool isControlling, DevicesList device, Vector3 position, Quaternion rotation, bool isMuted, bool isTeleportEnable, Transform parent)
    {
        Id = id;
        PhotonView = photonView;
        Name = name;
        Color = color;
        IsMaster = isMaster;
        IsControlling = isControlling;
        Device = device;
        Position = position;
        Rotation = rotation;
        IsMuted = isMuted;
        IsBLockedTp = isTeleportEnable;
        this.parent = parent;
        if (photonView != null)
            UserId = photonView.Owner.UserId;

    }
}
