using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoginController : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField senhaInput;
    public TMP_Text mensagemLogin; // texto para mensagens

    private string apiUrl = "https://api-vr-production.up.railway.app/login"; 
    // Ajuste se sua API for diferente

    public void FazerLogin()
    {
        mensagemLogin.text = "";

        string email = emailInput.text;
        string senha = senhaInput.text;

        if (email == "" || senha == "")
        {
            mensagemLogin.color = Color.red;
            mensagemLogin.text = "Preencha email e senha!";
            return;
        }

        StartCoroutine(EnviarLogin(email, senha));
    }

     public void LoginVisitante()
    {
        mensagemLogin.text = "";
        string email = "visitante@exemplo.com";
        string senha = "123456";

        StartCoroutine(EnviarLogin(email, senha));
    }

    IEnumerator EnviarLogin(string email, string senha)
    {
        LoginRequest reqBody = new LoginRequest
        {
            email = email,
            password = senha
        };

        string jsonData = JsonUtility.ToJson(reqBody);

        UnityWebRequest req = new UnityWebRequest(apiUrl, "POST");
        byte[] raw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        req.uploadHandler = new UploadHandlerRaw(raw);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            mensagemLogin.color = Color.green;
            mensagemLogin.text = "Autenticado com sucesso!";
            StartCoroutine(LimparMensagemDepoisDe5s(mensagemLogin));

            // Pega os dados de retorno da API
            string respostaJson = req.downloadHandler.text;
            Debug.Log("Login OK: " + respostaJson);

            // Salvar dados do usuário para a próxima cena
            PlayerPrefs.SetString("usuario_json", respostaJson);
            UsuarioData usuario = JsonUtility.FromJson<UsuarioData>(respostaJson);

            // Agora sim: SALVA o ID do usuário
            PlayerPrefs.SetInt("nomeID", usuario.nomeID);
            PlayerPrefs.SetString("nomeUsuario", usuario.nome);

            yield return new WaitForSeconds(1f);

            // Trocar de cena
            SceneManager.LoadScene("Cena_MenuInicial");
        }
        else
        {
            mensagemLogin.color = Color.red;
            mensagemLogin.text = "Erro: email ou senha incorretos";
            Debug.Log("ERRO LOGIN: " + req.downloadHandler.text);
            StartCoroutine(LimparMensagemDepoisDe5s(mensagemLogin));
        }
    }

    IEnumerator LimparMensagemDepoisDe5s(TMP_Text txt)
{
    yield return new WaitForSeconds(5f);
    txt.text = "";
}

}

[System.Serializable]
public class LoginRequest
{
    public string email;
    public string password;
}
