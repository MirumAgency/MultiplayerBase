#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Windows.Input;
public class AndroidPermission : MonoBehaviour
{
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
#if PLATFORM_ANDROID 
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }

#endif
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene("Connection");
    }
}
