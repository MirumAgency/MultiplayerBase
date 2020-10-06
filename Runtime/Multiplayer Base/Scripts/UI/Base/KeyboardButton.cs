using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(KeyboardButtonData))]
public class KeyboardButton : Button
{
    private KeyboardButtonData data;
    private bool isHover;

    protected override void Start()
    {
        base.Start();
        data = GetComponent<KeyboardButtonData>();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        data.Hover();
        isHover = true;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        data.Unhover();
        isHover = false;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        data.Press();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        if (isHover)
            data.Hover();
        else
            data.Unhover();
    }
}
