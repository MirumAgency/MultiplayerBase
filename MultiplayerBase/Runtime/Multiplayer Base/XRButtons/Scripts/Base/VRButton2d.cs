using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
[RequireComponent(typeof(AudioClip))]
public class VRButton2d : MonoBehaviour
{


    public UnityEvent OnPressButton, OnReleaseButton, OnToogleButton,OnButtonDisable;
    [SerializeField]
    private bool UseTag, ContinuosPress, ResetPosition, useToogle,startPress;
    [SerializeField]
    private string tag;
    [SerializeField]
    private float returnSpeed = 1, MaxMovement, sensibility = 0.40f;
    [SerializeField]
    private Image[] buttonComponentImage;
    [SerializeField]
    private Color hover, clic;
    [SerializeField]
    private AudioClip clicSound;
    [SerializeField]
    private Buttons2DGroup btonsgroup;
    private List<Color> defaulButtonComponentImageColor = new List<Color>();
    private AudioSource source;
    private bool justOne = true;
    private bool press;
    private float LastPosition;
    private Vector3 defaultPos;
    private bool justOneContinuePress;
    private bool enableContinuePress = true;
    private bool toogle = false;
    public Buttons2DGroup Btonsgroup
    {
        get
        {
            return btonsgroup;
        }

        set
        {
            btonsgroup = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        foreach (Image img in buttonComponentImage)
            defaulButtonComponentImageColor.Add(img.color);
        source = GetComponent<AudioSource>();
        defaultPos = transform.localPosition;
        if (startPress)
        {
            OnButtonDisable.Invoke();
            transform.localPosition = new Vector3(0, 0, MaxMovement);
            OnPressButton.Invoke();
            foreach (Image img in buttonComponentImage)
                img.color = clic;
        }

    }
    private void OnEnable()
    {
        LastPosition = 0;
        if (defaulButtonComponentImageColor.Count > 0)
        {
            for (int i = 0; i < buttonComponentImage.Length; i++)
            {
                buttonComponentImage[i].color = defaulButtonComponentImageColor[i];
            }

            OnReleaseButton.Invoke();
        }
        transform.localPosition = defaultPos;
    }

    private void OnTriggerStay(Collider other)
    {
        if (UseTag)
        {
            if (other.CompareTag(tag))
            {

                if (!press)
                {
                    foreach (Image img in buttonComponentImage)
                        img.color = hover;
                }
                StopAllCoroutines();
                var distance = Mathf.Abs(Vector3.Distance(transform.localPosition, other.transform.position)) * -1;
                if (LastPosition == 0)
                    LastPosition = distance;
                var Movement = distance - LastPosition;
                if (distance != LastPosition && !press)
                {
                    var z = Mathf.Clamp((Movement + transform.localPosition.z) * sensibility, 0, MaxMovement);
                    transform.localPosition = new Vector3(0, 0, z);
                }
                LastPosition = distance;
                if (transform.localPosition.z >= (MaxMovement))
                {
                    if (ContinuosPress)
                    {

                        if (btonsgroup)
                            btonsgroup.ResetPositions(this);

                        foreach (Image img in buttonComponentImage)
                            img.color = clic;
                        if (enableContinuePress)
                        {
                            Invoke("ContinuePressInterval", 0.1f);
                            //Debug.Log("Presionó el bton");
                            OnPressButton.Invoke();
                            enableContinuePress = false;
                        }
                    }
                    else
                    {
                        if (!press && justOne)
                        {
                            justOne = false;
                            OnPressButton.Invoke();
                            foreach (Image img in buttonComponentImage)
                                img.color = clic;
                            if (btonsgroup)
                                btonsgroup.ResetPositions(this);
                            Invoke("AlowClick", 0.5f);
                            source.clip = clicSound;
                            source.Play();
                            press = true;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (UseTag)
        {
            if (other.CompareTag(tag))
            {
                if (ResetPosition || !press)
                    ResetPositionButton();
                press = false;
            }
        }
    }

   
    public void ResetPositionButton()
    {
        LastPosition = 0;
        if (transform.localPosition.z != 0 && gameObject.activeSelf)
            StartCoroutine(ChangefloatOverTime());
        for (int i = 0; i < buttonComponentImage.Length; i++)
            buttonComponentImage[i].color = defaulButtonComponentImageColor[i];
        press = false;
        OnReleaseButton.Invoke();
    }

    private void AlowClick()
    {
        justOne = true;
    }
    private void ContinuePressInterval()
    {
        enableContinuePress = true;

    }
    private IEnumerator ChangefloatOverTime()
    {
        var t = 0f;
        while (t < 1)
        {
            var p = transform.localPosition.z;
            t += Time.deltaTime * returnSpeed;
            var z = Mathf.Lerp(p, 0, t);
            transform.localPosition = new Vector3(0, 0, z);
            yield return null;
        }
        transform.localPosition += new Vector3(0, 0, 0);
    }

   
}
