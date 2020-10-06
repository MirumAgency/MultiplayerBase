using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
public class ValidatePass : MonoBehaviour
{
    [SerializeField]
    private InputField pass;
    [SerializeField]
    private string password;
    [SerializeField]
    private UnityEvent OnPassCorrect,OnPassIncorrect;
    public void CheckPass()
    {
        if (pass.text == password)
            OnPassCorrect.Invoke();
        else {
            OnPassIncorrect.Invoke();
            pass.text = "";
        }
    }

}
