using EasyButtons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PreConfigSceneByDevice : MonoBehaviour
{
    [SerializeField]
    private DevicesList device;
    [SerializeField]
    private UnityEvent configOnQuest, configOnAndroid, configOnStandalone, configOnWindowsVive, configOnWindowsRift;

    [Button]
    private void SetConfigToAndroid()
    {
        device = DevicesList.Android;
        configOnAndroid.Invoke();
    }
    [Button]
    private void SetConfigToQuest()
    {
        Debug.Log("aljkfhsfgilug");
        device = DevicesList.OculusQuest;
        configOnQuest.Invoke();
    }
    [Button]
    private void SetConfigToWindowsRift()
    {
        device = DevicesList.OculusRift;
        configOnWindowsRift.Invoke();
    }
    [Button]
    private void SetConfigToWindowsVive()
    {
        device = DevicesList.HTCVive;
        configOnWindowsVive.Invoke();
    }
    [Button]
    private void SetConfigToStandalone()
    {
        device = DevicesList.Standalone;
        configOnStandalone.Invoke();
    }
}
