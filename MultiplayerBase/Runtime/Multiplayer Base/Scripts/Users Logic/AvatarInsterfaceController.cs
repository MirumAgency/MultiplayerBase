using Photon.Pun;
using Photon.Voice.PUN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarInsterfaceController : MonoBehaviour
{
    [SerializeField]
    private Color speakingColor, normalColor;
    [SerializeField]
    private Image MuteStatusImage, TeleportStatusImage,BackgroundTextImage;
    [SerializeField]
    private GameObject ControlStatusGameObject;
    [SerializeField]
    private Text nameAvatarText;
    [SerializeField]
    private Sprite muteOnSprite, muteOffSprite, tpOnsprite, tpOffSprite;
    [SerializeField]
    private PhotonVoiceView photonVoice;
    [SerializeField]
    private PhotonView photonView;

    private void Start()
    {
        OnlineUserInformation.Instance.OnRefreshPlayerInfo += OnChangePlayerInfo;
    }
    private void Update()
    {
        if (photonVoice.IsSpeaking)
            BackgroundTextImage.color = speakingColor;
        else
            BackgroundTextImage.color = normalColor;
    }
    public void SetupAvatarInterface (string name, bool isControling, bool isMuted,bool isTpBloked)
    {
        nameAvatarText.text = name;
        ManageSpriteControl(isControling);
        ManageSpriteMute(isMuted);
        ManageSpriteTeleport(isTpBloked);
    }
    public void OnChangePlayerInfo()
    {
        var myPlayer = OnlineUserInformation.Instance.ReturnPlayerById(photonView.ViewID);
        if (myPlayer != null)
        {
            ManageSpriteControl(myPlayer.IsControlling);
            ManageSpriteMute(myPlayer.IsMuted);
            ManageSpriteTeleport(myPlayer.IsBLockedTp);
        }
    }

    public void ManageSpriteControl (bool isControling)
    {
        if (isControling)
            ControlStatusGameObject.SetActive(true);
        else
            ControlStatusGameObject.SetActive(false);
    }
    public void ManageSpriteMute (bool isMuted)
    {
        if (isMuted)
            MuteStatusImage.sprite = muteOffSprite;
        else
            MuteStatusImage.sprite = muteOnSprite;
    }
    public void ManageSpriteTeleport (bool isTpBloked)
    {
        if (isTpBloked)
            TeleportStatusImage.sprite = tpOffSprite;
        else
            TeleportStatusImage.sprite = tpOnsprite;
    }
    private void OnDestroy()
    {
        PhotonEventManager.Instance.OnReceiveControlStatus -= ManageSpriteControl;
        PhotonEventManager.Instance.OnReceiveMuteStatus -= ManageSpriteMute;
        PhotonEventManager.Instance.OnReceiveTeleportStatus -= ManageSpriteTeleport;
    }
}
