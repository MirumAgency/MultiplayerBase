using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class CanvasKeyboard : MonoBehaviour
{
    [SerializeField]
    private CanvasKeyboardASCII ascii;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private string showAnimationKey = "IsShow";

    private InputField cachedInputField;

    public string Text
    {
        get
        {
            if (cachedInputField != null)
                return cachedInputField.text;

            return string.Empty;
        }

        set
        {
            if (cachedInputField != null)
                cachedInputField.text = value;
        }
    }

    public CanvasKeyboardASCII Ascii
    {
        get => ascii;
    }

    public void Show(InputField inputfield)
    {
        ascii.ActiveBoardUnshifted();
        anim.SetBool(showAnimationKey, true);
        AssingInput(inputfield);
    }

    public void Hide()
    {
        anim.SetBool(showAnimationKey, false);
    }

    #region Keyboard Receiving Input
    public void SendKeyString(string keyString)
    {
        if (keyString.Length == 1 && keyString[0] == 8 /*ASCII.Backspace*/)
        {
            Debug.Log("keyString " + keyString);
            //if (text.Length > 0)
            if (keyString.Equals("\x08"))
            {
                //text = text.Remove(text.Length - 1);
                cachedInputField.text = cachedInputField.text.Remove(cachedInputField.text.Length - 1);
                Text = cachedInputField.text;
            }
        }
        else
            Text += keyString;

        // Workaround: Restore focus to input fields (because Unity UI buttons always steal focus)
        ReactivateInputField(cachedInputField); 
    }

    private void AssingInput(InputField inputfield)
    {
        cachedInputField = inputfield;
        cachedInputField.text = Text = inputfield.text;
    }
    #endregion


    #region Steal Focus Workaround

    private void ReactivateInputField(InputField inputField)
    {
        if (inputField != null)
            StartCoroutine(ActivateInputFieldWithoutSelection(inputField));
    }

    IEnumerator ActivateInputFieldWithoutSelection(InputField inputField)
    {
        inputField.ActivateInputField();

        // wait for the activation to occur in a lateupdate
        yield return new WaitForEndOfFrame();

        // make sure we're still the active ui
        if (EventSystem.current.currentSelectedGameObject == inputField.gameObject)
        {
            // To remove hilight we'll just show the caret at the end of the line
            inputField.MoveTextEnd(false);
        }
    }
    #endregion
}
