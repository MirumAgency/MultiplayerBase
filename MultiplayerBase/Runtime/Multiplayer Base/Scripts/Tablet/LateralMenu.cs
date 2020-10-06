using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LateralMenu : MonoBehaviour
{
  
    [SerializeField]
    private List<Texture> emmisionOutlines;//0 home,1 experience,2 liveSession
    [SerializeField]
    private List<Color> colorEmmision;
    [SerializeField]
    private string texturePropety, colorEmmisionPropety;
    [SerializeField]
    private Material panelTablet;
    
    public UnityEvent OnHome, OnExperience, OnLiveSession;
    // Start is called before the first frame update
    void Start()
    {
        panelTablet.SetTexture(texturePropety, emmisionOutlines[2]);
        panelTablet.SetColor(colorEmmisionPropety, colorEmmision[2]);
    }

    public void Home()
    {
       
    }

    public void ChangeColor(int value)
    {
        panelTablet.SetTexture(texturePropety, emmisionOutlines[value]);
        panelTablet.SetColor(colorEmmisionPropety, colorEmmision[value]);
    }
    public void Experience()
    {
       
    }

    public void LiveSession()
    {
       
    }
}
