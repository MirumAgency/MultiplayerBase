using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMode : MonoBehaviour
{
    [SerializeField]
    private Canvas myCanvas;

    [SerializeField]
    private BaseCanvas _baseCanvas;

    public BaseCanvas BaseCanvas { get => _baseCanvas; set => _baseCanvas = value; }
    public Canvas MyCanvas { get => myCanvas; set => myCanvas = value; }

    public void SetVRCanvas()
    {
        MyCanvas.renderMode = RenderMode.WorldSpace;
    }

    public void SetNormalCanvas()
    {
        MyCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
    }

    public void SetVRCanvas(float size)
    {
        MyCanvas.renderMode = RenderMode.WorldSpace;
        SetCanvasSize(size);
    }

    public void SetNormalCanvas(float size)
    {
        MyCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        SetCanvasSize(size);
    }

    public void SetCanvasSize(float size)
    {
        RectTransform rectTransform = MyCanvas.GetComponent<RectTransform>();
        rectTransform.localScale = new Vector3(-size, size, size);
        rectTransform.localPosition = Vector3.zero;
    }

}
