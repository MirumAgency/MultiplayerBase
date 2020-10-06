using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObscureZoneController : MonoBehaviour
{
    public static ObscureZoneController Instance;
    public Action OnFadeInEnded, OnFadeOutEnded;
    [SerializeField]
    private Material mat;
    [SerializeField]
    private float speed = 1, t = 0;

    bool fadeIn, fadeOut;
    private void Awake()
    {
        if (ObscureZoneController.Instance == null)
            ObscureZoneController.Instance = this;
        else
            Destroy(this.gameObject);
    }
    private void Start()
    {
        mat.SetFloat("_Alpha", 1);
        StartCoroutine(WaitForInstance());
    }
    private IEnumerator WaitForInstance()
    {
        yield return new WaitUntil(() => Camera.main != null);
        transform.parent = Camera.main.transform;
        transform.localPosition = Vector3.zero;
    }

    [EasyButtons.Button]
    public void FadeIn()
    {
        t = 0;
        fadeIn = true;
    }
    [EasyButtons.Button]
    public void FadeOut()
    {
        t = 1;
        fadeOut = true;
    }
    private void Update()
    {
        if (fadeIn)
        {
            if (t < 1)
            {
                t += Time.deltaTime / speed;
                mat.SetFloat("_Alpha", t);
            }
            else
            {
                fadeIn = false;
                if (OnFadeInEnded != null)
                    OnFadeInEnded.Invoke();
            }
        }
        if (fadeOut)
        {
            if (t >= 0)
            {
                t -= Time.deltaTime / speed;
                mat.SetFloat("_Alpha", t);
            }
            else
            {
                fadeOut = false;
                OnFadeOutEnded?.Invoke();
            }
        }
    }
}
