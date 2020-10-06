using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PersistentGameObject : MonoBehaviour
{
    [SerializeField]
    private bool willBeExitsOnDisconect = false;

    public static List<PersistentObject> PersistentGameObjects = new List<PersistentObject>();
    private void Awake()
    {
        //Invoke("WaitToLoad", 2);
        WaitToLoad();
    }

    private void Start()
    {
        if (ExceptionsBehaviour.Instance)
            ExceptionsBehaviour.Instance.OnUserDisconnected += OnUserDisconnected;
    }

    private void WaitToLoad()
    {
        DontDestroyOnLoad(gameObject);
        PersistentObject persistentObject = PersistentGameObjects.Find(x => x.Name == gameObject.name);
        if (persistentObject == null)
        {
            PersistentGameObjects.Add(new PersistentObject(gameObject, gameObject.name));
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void OnUserDisconnected(string cause)
    {
        if (willBeExitsOnDisconect)
            return;

        ExceptionsBehaviour.Instance.ObjToDestroy.Add(gameObject);
        ExceptionsBehaviour.Instance.OnUserDisconnected -= OnUserDisconnected;

        PersistentObject persistentObject = PersistentGameObjects.Find(x => x.Name == gameObject.name);
        if (persistentObject != null)
            PersistentGameObjects.Remove(persistentObject);
        
        Destroy(this);
    }

    public void DestroyObj()
    {
        if (willBeExitsOnDisconect)
            return;

        Destroy(gameObject);
    }

    private void OnDestroy()
    {

    }
}
public class PersistentObject
{
    public GameObject GameObject;
    public string Name;

    public PersistentObject(GameObject gameObject, string name)
    {
        GameObject = gameObject;
        Name = name;
    }
}
