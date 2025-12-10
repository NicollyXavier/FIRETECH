using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ResultadoPayload
{
    public int nomeID;
    public int acertos;
    public int erros;
    public int pontuacao;
}

public class EnviarResultado : MonoBehaviour
{
    [Header("Config API")]
    public string baseApiUrl = "https://api-vr-production.up.railway.app/resultados";

    [Header("UI")]
    public Button botaoEnviar;
    public TextMeshProUGUI txtStatus;
    
    private UsuarioData usuarioLogado;

    public void OnClickEnviarResultado()
    {
        if (botaoEnviar != null) botaoEnviar.interactable = false;
        StartCoroutine(EnviarCoroutine());
    }

    IEnumerator EnviarCoroutine()
    {
        // Primeiro: puxar usuário logado
        string jsonUsuario = PlayerPrefs.GetString("usuario_json", "");
        
        if (string.IsNullOrEmpty(jsonUsuario))
        {
            Debug.LogError("ERRO: Nenhum usuário logado para enviar resultado!");
            if (txtStatus != null) txtStatus.text = "Erro: usuário não logado.";
            if (botaoEnviar != null) botaoEnviar.interactable = true;
            yield break;
        }


        usuarioLogado = JsonUtility.FromJson<UsuarioData>(jsonUsuario);

        Debug.Log("Usuário carregado:");
        Debug.Log("nomeID = " + usuarioLogado.nomeID);
        Debug.Log("nome = " + usuarioLogado.nome);
        Debug.Log("email = " + usuarioLogado.email);

        int nomeID = usuarioLogado.nomeID;

        Debug.Log("========== INICIANDO ENVIO ==========");
        Debug.Log($"nomeID recuperado: {nomeID}");

        if (AcertosErrosController.Instance == null)
        {
            Debug.LogError("ERRO: AcertosErrosController.Instance é null!");
            if (txtStatus != null) txtStatus.text = "Erro interno.";
            if (botaoEnviar != null) botaoEnviar.interactable = true;
            yield break;
        }

        // Agora montar o payload no FORMATO CORRETO
        ResultadoPayload payload = new ResultadoPayload
        {
            nomeID = nomeID,
            acertos = AcertosErrosController.Instance.acertos,
            erros = AcertosErrosController.Instance.erros,
            pontuacao = AcertosErrosController.Instance.pontuacao
        };

        string json = JsonUtility.ToJson(payload);

        Debug.Log("========== DADOS DA REQUISIÇÃO ==========");
        Debug.Log($"JSON enviado: {json}");
        Debug.Log("=========================================");

        using (UnityWebRequest req = new UnityWebRequest(baseApiUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            req.uploadHandler = new UploadHandlerRaw(bodyRaw);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            Debug.Log("Enviando requisição...");
            yield return req.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
            bool isError = req.result == UnityWebRequest.Result.ConnectionError ||
                           req.result == UnityWebRequest.Result.ProtocolError;
#else
            bool isError = req.isNetworkError || req.isHttpError;
#endif

            Debug.Log("========== RESPOSTA DO SERVIDOR ==========");
            Debug.Log($"Status Code: {req.responseCode}");
            Debug.Log($"Response Body: {req.downloadHandler.text}");
            Debug.Log("==========================================");

            if (isError)
            {
                Debug.LogError($"ERRO {req.responseCode}: {req.downloadHandler.text}");
                if (txtStatus != null) txtStatus.text = $"Erro: {req.responseCode}";
                if (botaoEnviar != null) botaoEnviar.interactable = true;
            }
            else
            {
                Debug.Log("SUCESSO!");
                if (txtStatus != null) txtStatus.text = "Resultado enviado!";
                AcertosErrosController.Instance.ResetAll();
                yield return new WaitForSeconds(1f);
                UnityEngine.SceneManagement.SceneManager.LoadScene("Cena_MenuInicial");
            }
        }
    }
}
