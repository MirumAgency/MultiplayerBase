using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using System.Collections;
using UnityEngine.SceneManagement;
public class AvatarReferences : MonoBehaviour
{
    [SerializeField]
    private Transform avatarHead, avatarBody, avatarHandLeft, avatarHandRigth, uiParent;

    [SerializeField]
    private Text nameAvatar;

    [SerializeField]
    private PhotonView photonView;

    [SerializeField]
    private Renderer rendererBody, RendererHead;

    [SerializeField]
    private AvatarInsterfaceController avatarInsterfaceController;

    [SerializeField]
    private DevicesList deviceAvatar;
    [SerializeField]
    private float headBodyDistance = 1f, uiHeadDistance = 0.979367f;

    private Color avatarColor;
    private float maxHeadAngle = 0.2f;
    public Text NameAvatar { get => nameAvatar; set => nameAvatar = value; }
    public Transform AvatarHead { get => avatarHead; set => avatarHead = value; }
    public Transform AvatarBody { get => avatarBody; set => avatarBody = value; }
    public Transform AvatarHandLeft { get => avatarHandLeft; set => avatarHandLeft = value; }
    public Transform AvatarHandRigth { get => avatarHandRigth; set => avatarHandRigth = value; }

    public void SetupInstantiation(PhotonView photonView, Color color, DevicesList device)
    {
        this.photonView = photonView;
        avatarColor = color;
        //setear localmente el device del avatar
        deviceAvatar = device;
        //Insertar nuevo usuario en la lista de conectados
        InsertUserInListOnline();
        //Definir acciones que se hacen si el avatar es mio o de otro cliente
        if (photonView.IsMine)
            OnMineAvatar();
        else
            OnClientAvatar();
        //definir que acciones se hacen en el robot diferenciando si es el amster o el client
        if (photonView.Owner.IsMasterClient)
            OnMasterUser();
        else
            OnClientUser();
    }
    private void OnMineAvatar()
    {
        //Config avatar preferences
        avatarInsterfaceController.gameObject.SetActive(false);
        rendererBody.enabled = false;
        RendererHead.enabled = false;
        nameAvatar.text = PhotonNetwork.NickName;
        transform.parent = ParentAvatarsManager.Instance.GetParentByDevice();
        transform.localPosition = Vector3.zero;
        ObscureZoneController.Instance.FadeOut();
    }
    private void OnClientAvatar()
    {
        transform.parent = ParentAvatarsManager.Instance.AvatarClientsParent;
        nameAvatar.text = photonView.Owner.NickName;        
    }

    [PunRPC]
    public void RequestCurrentScene()
    {
        Debug.Log("Me llego un request por la scena");
        StartCoroutine(WaitForSceneChange());
    }
    [PunRPC]
    public void ReturnCurrentScene(string sceneName)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.Log("El master esta en la scena: " + sceneName);
            if (!SceneManager.GetActiveScene().name.Equals(sceneName))
            {
                Debug.Log("Sync scene with master");
                SceneManager.LoadScene(sceneName);
            }
        }        
    }

    private IEnumerator WaitForSceneChange ()
    {
        var LocalScene = FindObjectOfType<MasterChangeScene>();
        Debug.Log("waiting for bool  " + LocalScene.LevelLoaded);
        yield return new WaitUntil(()=> LocalScene.LevelLoaded);
        yield return new WaitForSeconds(1);
        Debug.Log("retorne la scena: " + SceneManager.GetActiveScene().name);
        photonView.RPC("ReturnCurrentScene", RpcTarget.All, SceneManager.GetActiveScene().name);
    }
    private void OnMasterUser()
    {
        rendererBody.material.SetColor("_Color", Color.red);
    }
    private void OnClientUser()
    {
        Debug.Log(deviceAvatar);
        avatarHandRigth.gameObject.SetActive(DisableHandsByDevice());
        avatarHandLeft.gameObject.SetActive(DisableHandsByDevice());
        rendererBody.material.SetColor("_Color", avatarColor);        
    }
    private bool DisableHandsByDevice()
    {
        switch (deviceAvatar)
        {
            case DevicesList.None:
                return false;
            case DevicesList.HTCVive:
                return true;
            case DevicesList.OculusRift:
                return true;
            case DevicesList.OculusQuest:
                return true;
            case DevicesList.DayDream:
                return false;
            case DevicesList.Android:
                return false;
            case DevicesList.Standalone:
                return false;
        }
        return false;
    }
    private void InsertUserInListOnline()
    {
        PlayerInformation playerInfo = new PlayerInformation(photonView.ViewID, photonView, photonView.Owner.NickName,
            avatarColor, photonView.Owner.IsMasterClient, photonView.Owner.IsMasterClient, deviceAvatar,
            ParentAvatarsManager.Instance.GetParentByDevice().position,
            Quaternion.identity, false, false, ParentAvatarsManager.Instance.GetParentByDevice());
        avatarInsterfaceController.SetupAvatarInterface(playerInfo.Name, playerInfo.IsControlling, playerInfo.IsMuted, playerInfo.IsBLockedTp);
        OnlineUserInformation.Instance.AddPlayer(playerInfo);
        if (photonView.IsMine)
        {
            playerInfo.avatarReferences = this;
            OnlineUserInformation.Instance.LocalPlayerInformation = playerInfo;
            PhotonEventManager.Instance.OnInstanceMyAvatar?.Invoke();
            if (!PhotonNetwork.IsMasterClient)
            {
                photonView.RPC("RequestCurrentScene", RpcTarget.MasterClient);
            }
        }
    }
    private void Update()
    {
        SetBodyTransform();
        NameLookAtCamera();
        SetUiTransform();
    }
    private void NameLookAtCamera()
    {
        if (Camera.main)
            uiParent.LookAt(new Vector3(Camera.main.transform.position.x, uiParent.position.y, Camera.main.transform.position.z));
    }
    private void SetBodyTransform()
    {
        if (photonView != null)
            if (photonView.IsMine)
            {
                //POSITION
                avatarBody.position = new Vector3(avatarHead.position.x, avatarHead.position.y - headBodyDistance, avatarHead.position.z);
                //ROTATION
                float bodyDirection = avatarHead.transform.rotation.y;
                float headDirection = avatarBody.transform.rotation.y;
                float angleBeetweenHeadAndBody = Mathf.Abs(bodyDirection - headDirection);
                if (angleBeetweenHeadAndBody > maxHeadAngle)
                {
                    Quaternion newBodyRotation = avatarHead.transform.rotation;
                    newBodyRotation.x = 0;
                    newBodyRotation.z = 0;
                    avatarBody.rotation = Quaternion.RotateTowards(avatarBody.rotation, newBodyRotation, Time.deltaTime * 200);
                }
            }
    }
    private void SetUiTransform()
    {
        //POSITION
        uiParent.position = new Vector3(avatarHead.position.x, avatarHead.position.y + uiHeadDistance, avatarHead.position.z);
    }
}
