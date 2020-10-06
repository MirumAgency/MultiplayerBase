using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadedFlag : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;
    void Start()
    {
        FindObjectOfType<MasterChangeScene>().FlagLevelLoaded(spawnPoint);
    }
}
