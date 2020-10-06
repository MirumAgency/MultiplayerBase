using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Toogle : MonoBehaviour
{
    public UnityEvent OnOptionA, OnOptionB;
    private bool toogle = false;
    public void OnToogle()
    {
        toogle =! toogle;
        if (toogle)
            OnOptionA.Invoke();
        else
            OnOptionB.Invoke();
    }

}
