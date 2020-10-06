using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiImageMultiLanguage : MonoBehaviour
{
    [SerializeField]
    private string key;
    [SerializeField]
    private Image image;

    public string Key { get => key; set => key = value; }
    public Image Image { get => image; set => image = value; }

    private void Start()
    {
        SetTextComponent();
    }
    public void SetTextComponent()
    {
        if (!Image)
            Image = GetComponent<Image>();
    }

    public void UpdateLanguage()
    {
        MultiLanguageController.Instance.UpdateAllLanguageComponents();
    }
}
