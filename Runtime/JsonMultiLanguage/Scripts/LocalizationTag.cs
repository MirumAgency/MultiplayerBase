using System;
using UnityEngine;

[Serializable]
public class LocalizationTag
{
    [SerializeField]
    private string tag;

    [SerializeField]
    private string value;

    public string Tag
    {
        get => tag;
    }

    public string Value
    {
        get => value;
    }
}