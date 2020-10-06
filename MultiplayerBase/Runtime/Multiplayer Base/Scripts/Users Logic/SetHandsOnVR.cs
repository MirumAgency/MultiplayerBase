using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHandsOnVR : MonoBehaviour
{
    [SerializeField]
    private Transform leftParent, rigthParent,HeadParetn,BodyParent;
    [SerializeField]
    private DevicesList device;
    void Awake()
    {
        if (device == DetectPlatform.Instance.DeviceDetected)
        {
            PhotonEventManager.Instance.OnInstanceMyAvatar += SetAvatarsComponentsToVRParents;
        }
    }

    public void SetAvatarsComponentsToVRParents()
    {
        OnlineUserInformation.Instance.LocalPlayerInformation.avatarReferences.AvatarBody.parent = BodyParent;
        OnlineUserInformation.Instance.LocalPlayerInformation.avatarReferences.AvatarHead.parent = HeadParetn;        
        OnlineUserInformation.Instance.LocalPlayerInformation.avatarReferences.AvatarHead.localPosition = Vector3.zero;
        OnlineUserInformation.Instance.LocalPlayerInformation.avatarReferences.AvatarHead.localRotation = Quaternion.Euler(0, 0, 0);
        OnlineUserInformation.Instance.LocalPlayerInformation.avatarReferences.AvatarHandRigth.parent = rigthParent;
        OnlineUserInformation.Instance.LocalPlayerInformation.avatarReferences.AvatarHandLeft.parent = leftParent;
        OnlineUserInformation.Instance.LocalPlayerInformation.avatarReferences.AvatarHandLeft.localPosition = Vector3.zero;
        OnlineUserInformation.Instance.LocalPlayerInformation.avatarReferences.AvatarHandLeft.localRotation = Quaternion.Euler(0,0,0);
        OnlineUserInformation.Instance.LocalPlayerInformation.avatarReferences.AvatarHandRigth.localPosition = Vector3.zero;
        OnlineUserInformation.Instance.LocalPlayerInformation.avatarReferences.AvatarHandRigth.localRotation = Quaternion.Euler(0,0,0);
    }
}
