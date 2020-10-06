using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisconnectCanvasBehaviour : MonoBehaviour
{
    [SerializeField]
    private bool setValuesInExceptionCAnvas = false;

    [SerializeField]
    private float canvasSize = 1;

    [SerializeField]
    private float distanceFromPlayer = 1;

    private Camera myCamera;

    public float CanvasSize { get => canvasSize; set => canvasSize = value; }
    public float DistanceFromPlayer { get => distanceFromPlayer; set => distanceFromPlayer = value; }

    // Start is called before the first frame update
    void Start()
    {
       
        ExceptionsBehaviour.Instance.OnUserDisconnected += OnUserDisconnected;
        StartCoroutine(WaitCamera());
    }

    IEnumerator WaitCamera()
    {
        yield return new WaitUntil(()=>Camera.main != null);
        myCamera = Camera.main;
        ExceptionsBehaviour.Instance.CanvasMode.MyCanvas.worldCamera = myCamera;
    }

    public void OnUserDisconnected(string cause)
    {
        DoSomenthingInCurrentPlatform();
    }

    private void DoSomenthingInCurrentPlatform()
    {
        DevicesList currentDevice = DetectPlatform.Instance.DeviceDetected;

        switch (currentDevice)
        {
            case DevicesList.None:
                break;
            case DevicesList.HTCVive:
                if(setValuesInExceptionCAnvas)
                {
                    ExceptionsBehaviour.Instance.CanvasMode.SetCanvasSize(canvasSize);
                    ExceptionsBehaviour.Instance.CanvasMode.BaseCanvas.DistanceFromPlayer = distanceFromPlayer;
                    
                }
                break;
            case DevicesList.OculusRift:
                if (setValuesInExceptionCAnvas)
                {
                    ExceptionsBehaviour.Instance.CanvasMode.SetCanvasSize(canvasSize);
                    ExceptionsBehaviour.Instance.CanvasMode.BaseCanvas.DistanceFromPlayer = distanceFromPlayer;
                }
                break;
            case DevicesList.OculusQuest:
                if (setValuesInExceptionCAnvas)
                {
                    ExceptionsBehaviour.Instance.CanvasMode.SetCanvasSize(canvasSize);
                    ExceptionsBehaviour.Instance.CanvasMode.BaseCanvas.DistanceFromPlayer = distanceFromPlayer;
                }
                break;
            case DevicesList.DayDream:
                break;
            case DevicesList.Android:
                break;
            case DevicesList.Standalone:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            default:
                break;
        }

    }
}
