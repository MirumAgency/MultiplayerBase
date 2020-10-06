#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using System;

public enum Languages
{
    EN,
    ES,
    NONE
}

public class MultiLanguageController : MonoBehaviour
{
    public event Action OnUpdated;

    [SerializeField]
    private string url;

    [SerializeField]
    private Dictionary<string, string> csvFile;

    [SerializeField]
    private Languages currentLanguage;

    [SerializeField]
    private List<LocalizationTag> tags;

    private string savePath;
    private static MultiLanguageController instance;

    public static MultiLanguageController Instance
    {
        get => instance;
    }

    public Languages CurrentLanguage
    {
        get => currentLanguage;
        set => currentLanguage = value;
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            ReadFile();
            UpdateAllLanguageComponents();
        }

    }

    [ContextMenu("ReadFile")]
    public void ReadFile()
    {
        string fileName = "Data";
        savePath = string.Format("{0}/{1}.csv", Application.dataPath + "/Resources", fileName);
        csvFile = CSVReader.ReadFile(fileName, (int)currentLanguage + 1);
    }

    public string LocalizeKey(string key)
    {
        string value = string.Empty;
        csvFile.TryGetValue(key, out value);
        return value;
    }

    public string LocalizeText(LocalizationKeys key)
    {
        return LocalizeText(key.ToString());
    }

    public string LocalizeText(string key)
    {
        string value = string.Empty;

        csvFile.TryGetValue(key, out value);

        if (value == string.Empty)
            value = "ERROR_LOCALIZATION";
        else
            value = FormatText(value);

        return value;
    }

    private string FormatText(string str)
    {
        for (int i = 0; i < tags.Count; i++)
            str = str.Replace(tags[i].Tag, Regex.Unescape(tags[i].Value));

        return str;
    }

    public AudioClip LocalizeSound(string key)
    {
        AudioClip audio;
        audio = Resources.Load($"Sounds/{key}{ReturnStringLanguage()}") as AudioClip;
        if (!audio)
            audio = Resources.Load($"Sounds/404Audio") as AudioClip;
        return audio;
    }

    private Sprite LocalizeImage(string key)
    {
        Texture2D tex;
        tex = Resources.Load($"Sprites/{key}{ReturnStringLanguage()}") as Texture2D;
        if (tex)
            return Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        else
        {
            tex = Resources.Load($"Sprites/404Image") as Texture2D;
            return Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }

    private string ReturnStringLanguage()
    {
        var language = "";
        switch (currentLanguage)
        {
            case Languages.EN:
                language = "_En";
                break;
            case Languages.ES:
                language = "_Es";
                break;
        }
        return language;
    }

    [EasyButtons.Button]
    public void UpdateBaseComponent()
    {
        BaseMultiLanguage[] texts = (BaseMultiLanguage[])Resources.FindObjectsOfTypeAll(typeof(BaseMultiLanguage));

        foreach (var t in texts)
        {
            t.UpdateLanguage();
        }
    }

    public void UpdateLanguageAudio()
    {
        AudioSourceMultiLanguage[] texts = (AudioSourceMultiLanguage[])Resources.FindObjectsOfTypeAll(typeof(AudioSourceMultiLanguage));
        foreach (var t in texts)
        {
            var key = t.Key;
            if (!string.IsNullOrEmpty(key))
                if (t.Audio)
                    t.Audio.clip = LocalizeSound(key);
        }
    }

    public void UpdateSpecificLanguageAudio(AudioSourceMultiLanguage language)
    {
        if (!string.IsNullOrEmpty(language.Key))
            if (language.Audio)
                language.Audio.clip = LocalizeSound(language.Key);
    }

    public void UpdateLanguageSprite()
    {
        UiImageMultiLanguage[] texts = (UiImageMultiLanguage[])Resources.FindObjectsOfTypeAll(typeof(UiImageMultiLanguage));
        foreach (var t in texts)
        {
            var key = t.Key;
            if (!string.IsNullOrEmpty(key))
                t.Image.sprite = LocalizeImage(key);
        }
    }

    [EasyButtons.Button]
    public void UpdateAllLanguageComponents()
    {
        UpdateBaseComponent();
        UpdateLanguageAudio();
        UpdateLanguageSprite();

        if (OnUpdated != null)
            OnUpdated();
    }

    [EasyButtons.Button]
    public void DownloadText()
    {
        StartCoroutine(GetText("Data"));
    }

    private IEnumerator GetText(string file_name)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                savePath = string.Format("{0}/{1}.csv", Application.dataPath + "/Resources", file_name);
                File.WriteAllText(savePath, www.downloadHandler.text);

                while (!File.Exists(savePath)) { }

                Dictionary<string, string> file = CSVReader.ReadFile("Data", 0);

                using (StreamWriter streamWriter = new StreamWriter("Assets/JsonMultiLanguage/Scripts/LocalizationKeys.cs"))
                {
                    streamWriter.WriteLine("public enum LocalizationKeys");
                    streamWriter.WriteLine("{");

                    List<string> keys = new List<string>(file.Keys);

                    for (int i = 1; i < keys.Count; i++)
                    {
                        streamWriter.WriteLine("\t" + keys[i] + ",");
                    }

                    streamWriter.WriteLine("}");
                }

#if UNITY_EDITOR
                AssetDatabase.Refresh();
#endif
            }
        }
    }

    public AudioClip GetAudioClip(string key)
    {
        return LocalizeSound(key);
    }
}