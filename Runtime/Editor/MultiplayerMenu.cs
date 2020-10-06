
using UnityEngine;
using UnityEditor;

public class MultiplayerMenu : MonoBehaviour
{
    [MenuItem("Xennial Digital/Multiplayer/Add Essentials Items")]
    static void AddtaskManagerOnline()
    {
        var item = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load("SceneConfiguration/PhotonLevelConfig"));
        var item2 = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load("SceneConfiguration/PhotonPersistantConfiguration"));
    }
}
