using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;
using Photon.Realtime;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class ExceptionsBehaviour : MonoBehaviourPunCallbacks
{
    public Action<string> OnUserDisconnected;
    public Action OnUserLostSession;
    public Action<string> OnUserJoinRoomFailed;
    public Action<string> OnUserCreateRoomFailed;
    public Action<bool> OnUserIsAbsent;

    [SerializeField]
    private GameObject canvasExceptions;

    [SerializeField]
    private GameObject background;

    [SerializeField]
    private Text exceptionMessage;

    [SerializeField]
    private CanvasMode _canvasMode;

    private List<GameObject> objToDestroy = new List<GameObject>();
    private bool connectFirstTime = false;
    public static ExceptionsBehaviour Instance
    {
        get;
        private set;
    }
    public GameObject CanvasExceptions { get => canvasExceptions; set => canvasExceptions = value; }
    public GameObject Background { get => background; set => background = value; }
    public List<GameObject> ObjToDestroy { get => objToDestroy; set => objToDestroy = value; }
    public CanvasMode CanvasMode { get => _canvasMode; set => _canvasMode = value; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

    }

    private void SomethingWentWrong()
    {
        Background.SetActive(true);
        CanvasExceptions.SetActive(true);
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneIndex = currentScene.buildIndex;
        Debug.Log("Setear el canvas para cada plataforma");
    }

    public override void OnConnected()
    {
        connectFirstTime = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnectedd(" + cause + ")");
        OnlineUserInformation.Instance = null;
        exceptionMessage.text = DisconectText(cause);
        SomethingWentWrong();
        OnUserDisconnected?.Invoke(cause.ToString());
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRoomFailed(" + message + ")");
        exceptionMessage.text = message;
        SomethingWentWrong();
        OnUserJoinRoomFailed?.Invoke(message);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed(" + message + ")");
        //SomethingWentWrong();
        OnUserCreateRoomFailed?.Invoke(message);
    }

    public void DestroyPersitentObjects()
    {
        int count = objToDestroy.Count;
        for (int i = 0; i < count; i++)
        {
            Destroy(objToDestroy[i]);
        }
        GameObject b = GameObject.Find("Photon Voice");

        if (b != null)
            Destroy(b);

        objToDestroy.Clear();
    }

    public void OnApplicationPause(bool pause)
    {
        OnUserIsAbsent?.Invoke(pause);
        /*if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("Session Lost");
            OnUserLostSession?.Invoke();
        }*/
    }

    string DisconectText(DisconnectCause cause)
    {
        switch (cause)
        {
            case DisconnectCause.None:
                return "Opps something went wrong with your connection, please try again. ";
            case DisconnectCause.ExceptionOnConnect:
                return "Opps, something went wrong with your connection, please try again. ";
            case DisconnectCause.Exception:
                return "Opps, something went wrong with your connection, please try again. ";
            case DisconnectCause.ServerTimeout:
                return "Opps, something went wrong with your connection, please try again. ";
            case DisconnectCause.ClientTimeout:
                return "Opps, something went wrong with your connection, please try again. ";
            case DisconnectCause.DisconnectByServerLogic:
                return "Opps, something went wrong with your connection, please try again. ";
            case DisconnectCause.DisconnectByServerReasonUnknown:
                return "Opps, something went wrong with your connection, please try again. ";
            case DisconnectCause.InvalidAuthentication:
                return "Opps, something went wrong with your connection, please try again. ";
            case DisconnectCause.CustomAuthenticationFailed:
                return "Opps, something went wrong with your connection, please try again. ";
            case DisconnectCause.AuthenticationTicketExpired:
                return "Opps, something went wrong with your connection, please try again. ";
            case DisconnectCause.MaxCcuReached:
                return "Opps, something went wrong with your connection, please try again. ";
            case DisconnectCause.InvalidRegion:
                return "Opps, something went wrong with your connection, please try again. ";
            case DisconnectCause.OperationNotAllowedInCurrentState:
                return "Opps, something went wrong with your connection, please try again. ";
            case DisconnectCause.DisconnectByClientLogic:
                return "The session has been finished by the host.";
            default:
                break;
        }
        return "";
    }



}
