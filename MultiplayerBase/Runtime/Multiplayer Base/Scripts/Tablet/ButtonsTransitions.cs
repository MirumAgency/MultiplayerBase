using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsTransitions : MonoBehaviour
{
    [SerializeField]
    private List<Transform> buttonsPanel;
    [SerializeField]
    [HideInInspector]
    private List<Vector3> defaultButtonsPos= new List<Vector3>();
    [SerializeField]
    [HideInInspector]
    private List<Vector3> targetButtonsPos = new List<Vector3>();
   
    [EasyButtons.Button]
    private void SaveDefaultButtonPos()
    {
        defaultButtonsPos.Clear();
        foreach (var btn in buttonsPanel)
            defaultButtonsPos.Add(btn.localPosition);
    }
    [EasyButtons.Button]
    private void SaveTargetButtonPos()
    {
        targetButtonsPos.Clear();
        foreach (var btn in buttonsPanel)
            targetButtonsPos.Add(btn.localPosition);
    }
    [EasyButtons.Button]
    public void SetDefaultPos()
    {
        for (int i = 0; i < buttonsPanel.Count; i++)
            buttonsPanel[i].localPosition = defaultButtonsPos[i];
    }
    [EasyButtons.Button]
    public void OnDisableButttons()
    {
        StartCoroutine(DisableAnim());
    }
    private IEnumerator DisableAnim()
    {
        float t = 0;
        float duration = 0.5f;
        while (t<duration)
        {
            for (int i = 0; i < buttonsPanel.Count; i++)
                buttonsPanel[i].localPosition = Vector3.Lerp(buttonsPanel[i].localPosition, targetButtonsPos[i], (t / duration));
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    [EasyButtons.Button]
    private void FindButtons()
    {
        buttonsPanel.Clear();
        foreach (Transform btn in transform)
            buttonsPanel.Add(btn.GetComponentInChildren<VRButton2d>().transform);

    }
    public void OnEnableButtons()
    {
        StartCoroutine(EnableAnim());
    }
    private IEnumerator EnableAnim()
    {
        float t = 0;
        float duration = 0.5f;
        while (t < duration)
        {
            
            for (int i = 0; i < buttonsPanel.Count; i++)
                buttonsPanel[i].localPosition = Vector3.Lerp(buttonsPanel[i].localPosition, defaultButtonsPos[i], (t / duration));

            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

}
