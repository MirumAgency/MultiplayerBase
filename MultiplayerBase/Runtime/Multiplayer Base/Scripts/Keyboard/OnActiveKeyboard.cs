using UnityEngine;
using UnityEngine.UI;

public class OnActiveKeyboard : MonoBehaviour
{
    [SerializeField]
    private CanvasKeyboard keyboard;

    public static OnActiveKeyboard Instance
    {
        get;
        private set;
    }

    public CanvasKeyboard Keyboard
    {
        get => keyboard;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void ShowKeyboard(InputField inputField)
    {
        keyboard.Show(inputField);
    }

    public void HideKeyboard()
    {
        keyboard.Hide();
    }
}