using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyButtons;

public class RestartScene : MonoBehaviour
{

    // Use this for initialization
    public int sceneIndex = 0;
    public float seconds = 10;

    private IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(seconds);
        //PhotonNetwork.LoadLevel(sceneIndex);
        SceneManager.LoadScene(0);
    }

    public void restart()
    {
        DeleteAll();
    }

    public void DeleteAll()
    {
        foreach (PersistentGameObject o in FindObjectsOfType<PersistentGameObject>())
        {
            o.OnUserDisconnected("None");
            o.DestroyObj();
        }

        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }

    [Button]
    public void Disconnect()
    {
        Debug.Log("Bye Bye");
        PhotonNetwork.Disconnect();
    }



}
