using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColorsBtnEvent : MonoBehaviour
{


    [SerializeField]
    private Image[] btnImg;
    [SerializeField]
    private Color colorInteract;
    
    private List<Color> defaultColors = new List<Color>();
    // Use this for initialization
    void Start()
    {
        foreach (Image img in btnImg)
            defaultColors.Add(img.color);
    }

    public void OnInteract()
    {
        foreach (Image img in btnImg)
            img.color = colorInteract;
    }
    public void OnDefault()
    {
        for (int i = 0; i < btnImg.Length; i++)
            btnImg[i].color = defaultColors[i];
    }
}
