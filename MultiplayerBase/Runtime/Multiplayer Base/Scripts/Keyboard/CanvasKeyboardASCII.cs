using UnityEngine;
using System;

public class CanvasKeyboardASCII : MonoBehaviour
{
    public event Action OnInvokeButton;

    public CanvasKeyboard canvasKeyboard;
    public GameObject alphaBoardUnsfhifted;
    public GameObject alphaBoardSfhifted;

    private bool shiftDown = false;

    private void Awake()
    {
        Refresh();
    }

    private void Refresh()
    {
        alphaBoardUnsfhifted.SetActive(!shiftDown);
        alphaBoardSfhifted.SetActive(shiftDown);
    }

    public void OnKeyDown(GameObject kb)
    {
        if (kb.name == "DONE")
        {
            if (canvasKeyboard != null)
                OnInvokeButton?.Invoke();
        }
        else if (kb.name == "SHIFT")
        {
            shiftDown = !shiftDown;
            Refresh();
        }
        else
        {
            if (canvasKeyboard != null)
            {
                string s;

                if (kb.name == "BACKSPACE")
                    s = "\x08";
                else
                    s = kb.name;

                canvasKeyboard.SendKeyString(s);
            }
        }
    }

    public void ActiveBoardUnshifted()
    {
        shiftDown = false;
        Refresh();
    }

    public void ActiveBoardSfhifted()
    {
        shiftDown = true;
        Refresh();
    }
}