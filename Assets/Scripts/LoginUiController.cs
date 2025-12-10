using TMPro;
using UnityEngine;

public class LoginUIController : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject registerPanel;
    public TMP_Text loginMessage;

    public void ShowRegister()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
    }

    public void ShowLogin()
    {
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
    }
}
