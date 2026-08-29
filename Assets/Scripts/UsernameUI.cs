using TMPro;
using UnityEngine;

public class UsernameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text usernameText;
    [SerializeField] private TMP_InputField usernameInputField;

    [SerializeField] private CloudServices cloudServices;

    private async void Start() 
    {

        try
        {
            await cloudServices.RealizarLogin();
            AtualizarUI();
        }
        catch (System.Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    private void AtualizarUI()
    {
        string username = cloudServices.GetUsername();
        usernameText.text = username;
        usernameInputField.text = username.Substring(0, username.IndexOf("#"));
    }

    public async void SalvarNovoUsername()
    {
        await cloudServices.AtualizarUsername(usernameInputField.text);

        AtualizarUI();
    }
}
