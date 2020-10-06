using UnityEngine;

public class DetectPlatform : MonoBehaviour
{
    [SerializeField]
    private DevicesList deviceDetected;
    public static DetectPlatform Instance
    {
        get;
        private set;
    }
    public DevicesList DeviceDetected { get => deviceDetected; set => deviceDetected = value; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void SetDeviceQuest()
    {
       deviceDetected = DevicesList.OculusQuest;
    }

    public void SetDeviceAndroid()
    {
        deviceDetected = DevicesList.Android;
    }

    public void SetDeviceVive()
    {
        deviceDetected = DevicesList.HTCVive;
    }

    public void SetDeviceRift()
    {
        deviceDetected = DevicesList.OculusRift;
    }

    public void SetDeviceStandalone()
    {
        deviceDetected = DevicesList.Standalone;
    }

}