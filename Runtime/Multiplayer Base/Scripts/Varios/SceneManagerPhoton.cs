using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;

public class SceneManagerPhoton : MonoBehaviour
{
    public void ChangeSceneByName (string name)
    {
        PhotonNetwork.LoadLevel(name);
    }
    public void ChangeSceneByIndex (int index)
    {
        PhotonNetwork.LoadLevel(index);
    }
}
