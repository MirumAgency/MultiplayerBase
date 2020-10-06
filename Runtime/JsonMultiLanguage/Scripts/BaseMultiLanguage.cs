using UnityEngine;
using UnityEngine.UI;

public abstract class BaseMultiLanguage : MonoBehaviour
{
    [SerializeField]
    protected Text text;

    protected virtual void Start()
    {
        UpdateLanguage();
    }

    public void SetTextComponent()
    {
        if (!text)
            text = GetComponent<Text>();
    }

     public abstract void UpdateLanguage();
}