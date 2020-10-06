using UnityEngine;
using UnityEngine.UI;

public class FocusInput : MonoBehaviour
{
    [SerializeField]
    private InputField inp;

    [SerializeField]
    private Button myButton;

    [SerializeField]
    private bool focused=true;

    private void OnEnable()
    {
        OnActiveKeyboard.Instance.Keyboard.Ascii.OnInvokeButton += OnInvokeButton;
        OnActiveKeyboard.Instance.ShowKeyboard(inp);
    }

    private void OnDisable()
    {
        OnActiveKeyboard.Instance.Keyboard.Ascii.OnInvokeButton -= OnInvokeButton;
        OnActiveKeyboard.Instance.HideKeyboard();
    }

    private void LateUpdate()
    {
        if (inp != null && focused)
        {
            bool isFocused = inp.isFocused;
            if (!isFocused)
                inp.Select();
        }

        if (gameObject.activeSelf && Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (myButton != null)
                myButton.onClick.Invoke();
        }
    }

    private void OnInvokeButton()
    {
        myButton.onClick.Invoke();
    }
}
