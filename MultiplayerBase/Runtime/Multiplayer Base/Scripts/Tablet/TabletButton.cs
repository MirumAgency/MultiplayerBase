using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TabletButton : MonoBehaviour
{
    [SerializeField]
    private TransitionTablet transitionTablet;
    public UnityEvent OnTransitionComplete;
    // Start is called before the first frame update
   
    public void OnAddEvent()
    {
        transitionTablet.OnFadeOutComplete -= OnClicEvent;
        transitionTablet.OnFadeOutComplete = OnClicEvent;
       
    }
   
    private void OnClicEvent()
    {
        OnTransitionComplete.Invoke();
    }

}
