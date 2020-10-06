using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChooseuserName : MonoBehaviour
{
    public UnityEvent OnSucces;
    public UnityEvent OnFailed;

    [SerializeField]
    private InputField input;

    [SerializeField]
    private Text message;

    [SerializeField]
    private int minCharacters=4,maxCharacters=9;

    [SerializeField]
    private GameObject showHost, hideInputName;

    private void Start()
    {
        string nickname = PhotonNetwork.NickName;
        if (!String.IsNullOrEmpty(nickname))
        {
            showHost.SetActive(true);
            hideInputName.SetActive(false);
        }

    }

    public void SetuserName()
    {
        string myText = input.text;
        string withoutSpace = myText.Replace(" ", ""); ;
        int countSpaces = withoutSpace.Length;
        if (countSpaces == 0)
        {
            message.text = "This field cannot be empty";
            OnFailed.Invoke();
        }
        else if (countSpaces < minCharacters)
        {
            message.text = string.Format("This field must have more than {0} characters", minCharacters);
            OnFailed.Invoke();
        }
        else if (countSpaces > maxCharacters)
        {
            message.text = string.Format("This field does not must have more than {0} characters", maxCharacters);
            OnFailed.Invoke();
        }
        else
        {
            message.text = "";
            PhotonNetwork.NickName = input.text;
            OnSucces.Invoke();
        }

        
    }
}
