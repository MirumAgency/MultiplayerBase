using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class DisconnectBehaviour : MonoBehaviour
{
    public UnityEvent OnDisconectEvent;
    public UnityEvent OnJoinRoomFailedEvent;
    public UnityEvent OnCreateRoomFailedEvent;
    public UnityEvent OnUserIsAbsentEvent;
    public UnityEvent OnUserIsNotAbsentEvent;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        ExceptionsBehaviour.Instance.OnUserDisconnected += OnUserDisconnected;
        ExceptionsBehaviour.Instance.OnUserJoinRoomFailed += OnJoinRoomFailed;
        ExceptionsBehaviour.Instance.OnUserCreateRoomFailed += OnUserCreateRoomFailed;
        ExceptionsBehaviour.Instance.OnUserIsAbsent += OnUserIsAbsent;
    }

    /// <summary>
    /// Call event if something went wrong with your connection
    /// </summary>
    /// <param name="cause"></param>
    public virtual void OnUserDisconnected(string cause)
    {
        OnDisconectEvent.Invoke();
    }

    /// <summary>
    /// Call event if something went wrong when you try to join into a room 
    /// </summary>
    /// <param name="cause"></param>
    public virtual void OnJoinRoomFailed(string cause)
    {
        OnJoinRoomFailedEvent.Invoke();
    }

    /// <summary>
    /// Call event if something went wrong when you try to create a room 
    /// </summary>
    /// <param name="cause"></param>
    public virtual void OnUserCreateRoomFailed(string cause)
    {
        Debug.Log("Calling OnCreateRoomFailedEvent");
        OnCreateRoomFailedEvent.Invoke();
    }

    /// <summary>
    /// Working in android
    /// </summary>
    /// <param name="value"></param>
    public virtual void OnUserIsAbsent(bool value)
    {
        //Debug.Log("Calling OnUserIsAbsentEvent or OnUserIsNotAbsentEvent" + value);
        if (value)//absent
            OnUserIsAbsentEvent.Invoke();
        else
            OnUserIsNotAbsentEvent.Invoke();
    }

    public virtual void OnDestroy()
    {

        ExceptionsBehaviour.Instance.OnUserDisconnected -= OnUserDisconnected;
        ExceptionsBehaviour.Instance.OnUserJoinRoomFailed -= OnJoinRoomFailed;
        ExceptionsBehaviour.Instance.OnUserCreateRoomFailed -= OnUserCreateRoomFailed;
        ExceptionsBehaviour.Instance.OnUserIsAbsent -= OnUserIsAbsent;
    }

}
