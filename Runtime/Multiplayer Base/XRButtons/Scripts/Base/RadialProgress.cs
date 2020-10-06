using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class RadialProgress : MonoBehaviour
{
    public UnityEvent OnProgressStart,OnProgressEnd;
    private Image loading;
    private float value;
    // Start is called before the first frame update
    void Start()
    {
        loading = GetComponent<Image>();
    }
    [EasyButtons.Button]
    public void OnProgress()
    {
        StopAllCoroutines();
        StartCoroutine(WaitForInteract());
    }

    private IEnumerator WaitForInteract()
    {
        value = 1;
        OnProgressStart.Invoke();
        while (value>=0)
        {
            value -= 0.005f;
            loading.fillAmount = value;
            yield return new WaitForEndOfFrame();
        }
        OnProgressEnd.Invoke();
    }
}
