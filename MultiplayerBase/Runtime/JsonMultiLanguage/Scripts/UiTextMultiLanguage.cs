using UnityEngine;
//using MyBox;

public class UiTextMultiLanguage : BaseMultiLanguage
{
    [SerializeField]
    private LocalizationKeys key;

    public bool usingString = false;
    //[ConditionalField("usingString")]
    [SerializeField]
    private string textKey;

    public LocalizationKeys Key { get => key; set => key = value; }

    public override void UpdateLanguage()
    {
        if (text != null)
        {
            if (usingString)
                text.text = MultiLanguageController.Instance.LocalizeText(textKey);
            else
                text.text = MultiLanguageController.Instance.LocalizeText(key);
        }

    }
}