using UnityEngine;
using UnityEngine.UI;

public class KeyboardButtonData : MonoBehaviour
{
    [SerializeField]
    private Sprite normalIcon;

    [SerializeField]
    private Sprite hoverIcon;

    [SerializeField]
    private Sprite pressIcon;

    [SerializeField]
    private Sprite normalBackground;

    [SerializeField]
    private Sprite pressBackground;

    [SerializeField]
    private Image background;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private string pressedAnimationKey = "IsPressed";

    public void Unhover()
    {
        background.sprite = normalBackground;
        icon.sprite = normalIcon;
        anim.SetBool(pressedAnimationKey, false);
    }

    public void Hover()
    {
        background.sprite = normalBackground;
        icon.sprite = hoverIcon;
        anim.SetBool(pressedAnimationKey, false);
    }

    public void Press()
    {
        background.sprite = pressBackground;
        icon.sprite = pressIcon;
        anim.SetBool(pressedAnimationKey, true);
    }
}
