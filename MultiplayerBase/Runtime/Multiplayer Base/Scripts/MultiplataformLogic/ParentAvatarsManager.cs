using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentAvatarsManager : MonoBehaviour
{
    [SerializeField]
    private Transform parentQuest, parentAndroid, parentWindowsVR, parentWindowsStandalone, avatarClientsParent;
    public Transform ParentQuest { get => parentQuest; set => parentQuest = value; }
    public Transform ParentAndroid { get => parentAndroid; set => parentAndroid = value; }
    public Transform ParentWindowsVR { get => parentWindowsVR; set => parentWindowsVR = value; }
    public Transform ParentWindowsStandalone { get => parentWindowsStandalone; set => parentWindowsStandalone = value; }
    public Transform AvatarClientsParent { get => avatarClientsParent; set => avatarClientsParent = value; }

    public static ParentAvatarsManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public Transform GetParentByDevice ()
    {
        switch (DetectPlatform.Instance.DeviceDetected)
        {
            case DevicesList.OculusRift:
                ParentWindowsVR.gameObject.SetActive(true);
                return ParentWindowsVR;
            case DevicesList.OculusQuest:
                ParentQuest.gameObject.SetActive(true);
                return ParentQuest;
            case DevicesList.Android:
                ParentAndroid.gameObject.SetActive(true);
                return ParentAndroid;
            case DevicesList.Standalone:
                ParentWindowsStandalone.gameObject.SetActive(true);
                return ParentWindowsStandalone;
        }
        return null;
    }
}
