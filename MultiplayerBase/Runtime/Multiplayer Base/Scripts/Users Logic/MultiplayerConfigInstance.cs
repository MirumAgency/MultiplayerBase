using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerConfigInstance : MonoBehaviour
{
    public static MultiplayerConfigInstance Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
