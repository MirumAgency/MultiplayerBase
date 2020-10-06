//using MyBox;
using UnityEngine;

public class AudioSourceMultiLanguage : MonoBehaviour
{

    //public bool ShowVariables = false;
    //[ConditionalField("ShowVariables")]
    [SerializeField]
    private string key;
    //[ConditionalField("ShowVariables")]
    [SerializeField]
    private AudioSource audio;

    public string Key { get => key; set => key = value; }
    public AudioSource Audio { get => audio; set => audio = value; }

    private void Start()
    {
        SetTextComponent();
    }

    public void PlayAudio()
    {
        audio.Play();
    }
    public void SetTextComponent()
    {
        if (!Audio)
            Audio = GetComponent<AudioSource>();
    }

    public void UpdateLanguage()
    {
        Debug.Log("task manager : KEYYYY " + key);
        MultiLanguageController.Instance.UpdateAllLanguageComponents();
    }

    public void UpdateSpesificAudio()
    {
        MultiLanguageController.Instance.UpdateSpecificLanguageAudio(this);
    }
}
