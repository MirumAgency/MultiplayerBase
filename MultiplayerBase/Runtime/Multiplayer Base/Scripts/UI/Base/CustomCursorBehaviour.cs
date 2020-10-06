using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomCursorBehaviour : Button
{
    protected RawImage myImage;
    protected Texture2D cursor1;

    protected override void Start()
    {
        myImage = GetComponent<RawImage>();
        base.Start();
        var cursorTexture = Resources.Load<Texture2D>("Textures/hand");
        cursor1 = cursorTexture;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (interactable)
            Cursor.SetCursor(cursor1, Vector2.zero, CursorMode.Auto);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (interactable)
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
