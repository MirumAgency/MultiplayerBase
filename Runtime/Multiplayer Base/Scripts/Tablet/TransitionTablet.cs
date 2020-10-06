using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class TransitionTablet : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    public Action OnFadeOutComplete;
    public UnityEvent OnFadeInStart, OnFadeInEnd, OnFadeOutStart, OnFadeOutEnd;

    private void OnEnable()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float value = 0;
        OnFadeInStart.Invoke();
        while (value < 1)
        {
            value += 0.05f;
            canvasGroup.alpha = value;
            yield return new WaitForEndOfFrame();
        }
        OnFadeInEnd.Invoke();
    }

    private IEnumerator FadeOut()
    {
        float value = 1;
        OnFadeOutStart.Invoke();
        while (value > 0)
        {
            value -= 0.05f;
            canvasGroup.alpha = value;
            yield return new WaitForEndOfFrame();
        }
        OnFadeOutEnd.Invoke();
        FadeOutComplete();
        gameObject.SetActive(false);

    }

    private void FadeOutComplete()
    {
        if (OnFadeOutComplete != null)
            OnFadeOutComplete();
    }

    [EasyButtons.Button]
    private void SetAlphaOne()
    {
        canvasGroup.alpha = 1;
    }
    [EasyButtons.Button]
    private void SetAlphaZero()
    {
        canvasGroup.alpha = 0;
    }
    [EasyButtons.Button]
    private void OnFadeIn()
    {
        StartCoroutine(FadeIn());
    }
    [EasyButtons.Button]
    public void OnFadeOut()
    {
        StartCoroutine(FadeOut());
    }
    

}
