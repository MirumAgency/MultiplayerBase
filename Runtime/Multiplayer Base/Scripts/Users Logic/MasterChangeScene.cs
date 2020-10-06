using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MasterChangeScene : MonoBehaviour
{
    private string sceneToLoad;
    [SerializeField]
    private PhotonView photonView;
    [SerializeField]
    private bool levelLoaded = false;

    public bool LevelLoaded { get => levelLoaded; set => levelLoaded = value; }

    private void Start()
    {
        StartCoroutine(WaitForInstanceZoneObscure());
    }
    [EasyButtons.Button]
    public void TestSceneChange()
    {
        StartChangeSceneProcess("level1Test");
    }
    [EasyButtons.Button]
    public void TestSceneChangeLobby()
    {
        StartChangeSceneProcess("TestLobbyScene");
    }
    public void StartChangeSceneProcess(string nameScene)
    {
        ControlEventsUsers.Instance.BlockTeleportAllPlayers(true);
        photonView.RPC("DoFadeIn", RpcTarget.All, nameScene);
    }

    [PunRPC]
    public void DoFadeIn(string nameScene)
    {
        sceneToLoad = nameScene;
        ObscureZoneController.Instance.FadeIn();
    }
    public void FinishFadeIn()
    {
        LevelLoaded = false;
        Debug.Log("chenking level");
        StartCoroutine(CheckLevelLoaded());
        PhotonNetwork.LoadLevel(sceneToLoad);
    }

    public void FlagLevelLoaded(Transform pocition)
    {
        StartCoroutine(WaitForInstance(pocition.position));        
    }
    private IEnumerator WaitForInstanceZoneObscure()
    {
        yield return new WaitUntil(() => ObscureZoneController.Instance != null);
        ObscureZoneController.Instance.OnFadeInEnded += FinishFadeIn;
    }
    private IEnumerator WaitForInstance(Vector3 pos)
    {
        yield return new WaitUntil(() => OnlineUserInformation.Instance != null);
        yield return new WaitUntil(() => OnlineUserInformation.Instance.LocalPlayerInformation != null);
        LevelLoaded = true;
        OnlineUserInformation.Instance.LocalPlayerInformation.parent.localPosition = pos;
        Debug.Log("change position to: " + pos);
        OnlineUserInformation.Instance.OnRefreshPlayerInfo?.Invoke();
        ObscureZoneController.Instance.FadeOut();
    }
    private IEnumerator CheckLevelLoaded()
    {
        yield return new WaitUntil(()=> !LevelLoaded);
        if (PhotonNetwork.IsMasterClient)
            ControlEventsUsers.Instance.BlockTeleportAllPlayers(false);
        ObscureZoneController.Instance.OnFadeInEnded -= FinishFadeIn;
        Debug.Log("FADE OUT");
        LevelLoaded = false;
    }

    private void OnDestroy()
    {
        ObscureZoneController.Instance.OnFadeInEnded -= FinishFadeIn;
    }
    private void OnLevelWasLoaded(int level)
    {
        ObscureZoneController.Instance.OnFadeInEnded -= FinishFadeIn;
    }
}
