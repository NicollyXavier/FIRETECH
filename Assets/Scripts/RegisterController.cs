using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class RegisterController : MonoBehaviour
{
    public TMP_InputField nomeInput;
    public TMP_InputField emailInput;
    public TMP_InputField senhaInput;

    public TMP_Text mensagem; // mensagem da tela de registro
    public LoginUIController uiController; // para acessar a tela de login

    private string apiUrl = "https://api-vr-production.up.railway.app/usuarios";

    public void Registrar()
    {
        mensagem.text = ""; // limpa ao clicar

        string nome = nomeInput.text;
        string email = emailInput.text;
        string senha = senhaInput.text;

        if (nome == "" || email == "" || senha == "")
        {
            mensagem.color = Color.red;
            mensagem.text = "Preencha todos os campos!";
            return;
        }

        Usuario novoUsuario = new Usuario
        {
            nome = nome,
            email = email,
            password = senha
        };

        string jsonData = JsonUtility.ToJson(novoUsuario);
        StartCoroutine(EnviarRegistro(jsonData));
    }

    IEnumerator EnviarRegistro(string json)
    {
        UnityWebRequest req = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        req.uploadHandler = new UploadHandlerRaw(bodyRaw);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            // mensagem na tela de registro
            mensagem.color = Color.green;
            mensagem.text = "Usuário registrado com sucesso!";
            StartCoroutine(LimparMensagemDepoisDe5s(mensagem));

            // espera 1 segundo para o usuário ler
            yield return new WaitForSeconds(1f);

            // limpa texto da tela de registro
            mensagem.text = "";

            // 🔥 LIMPA OS CAMPOS DO REGISTERPANEL
            nomeInput.text = "";
            emailInput.text = "";
            senhaInput.text = "";

            // mensagem na tela de login
            uiController.loginMessage.color = Color.green;
            uiController.loginMessage.text = "Conta criada com sucesso! Faça login.";

            // volta para o painel de login
            uiController.ShowLogin();
        }
        else
        {
            mensagem.color = Color.red;
            mensagem.text = "Erro ao registrar.";
            Debug.Log(req.downloadHandler.text);
            StartCoroutine(LimparMensagemDepoisDe5s(mensagem));
        }
    }
    IEnumerator LimparMensagemDepoisDe5s(TMP_Text txt)
{
    yield return new WaitForSeconds(5f);
    txt.text = "";
}

}

[System.Serializable]
public class Usuario
{
    public string nome;
    public string email;
    public string password;
}
