using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorTablet : MonoBehaviour
{
    [SerializeField]
    private Transform tabletMesh;
    [SerializeField]
    private List<Transform> canvasTablet;
    [SerializeField]
    private List<Vector3>leftHandledPos;
    [SerializeField]
    private List<Vector3> RightHandledPos;

    [EasyButtons.Button]
    private void SaveLeftHandled()
    {
        leftHandledPos.Clear();
        for (int i = 0; i < canvasTablet.Count; i++)
            leftHandledPos.Add(canvasTablet[i].localPosition);
    }
    [EasyButtons.Button]
    private void SaveRightHandled()
    {
        RightHandledPos.Clear();
        for (int i = 0; i <canvasTablet.Count; i++)
            RightHandledPos.Add(canvasTablet[i].localPosition);
    }
    [EasyButtons.Button]
    private void SetLeftHandled()
    {
        tabletMesh.localScale = Vector3.one*2;
        for (int i = 0; i < canvasTablet.Count; i++)
             canvasTablet[i].localPosition= leftHandledPos[i];
    }
    [EasyButtons.Button]
    private void SetRightHandled()
    {
        tabletMesh.localScale = new Vector3(2,2,-2);
        for (int i = 0; i < canvasTablet.Count; i++)
            canvasTablet[i].localPosition = RightHandledPos[i];
    }

}
