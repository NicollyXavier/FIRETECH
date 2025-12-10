using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class SceneEndUIController : MonoBehaviour
{
    [Header("Resultados")]
    public TextMeshProUGUI txtAcertos;
    public TextMeshProUGUI txtErros;
    public TextMeshProUGUI txtPontuacao;

    [Header("Login / Envio")]
    public Button botaoEnviar;
    public TextMeshProUGUI txtLoginAviso;

    [Header("Dados do Usuário")]
    public TextMeshProUGUI txtNomeUsuario; 
    public TextMeshProUGUI txtEmailUsuario; 

    private UsuarioData usuarioLogado;

    void Start()
    {
        CarregarUsuario();
        AtualizarUI();
    }

    void CarregarUsuario()
    {
        string json = PlayerPrefs.GetString("usuario_json", "");

        if (string.IsNullOrEmpty(json))
        {
            Debug.LogWarning("Nenhum usuário logado.");
            usuarioLogado = null;
            return;
        }

        usuarioLogado = JsonUtility.FromJson<UsuarioData>(json);

        if (usuarioLogado != null)
        {
            Debug.Log("Usuário logado:");
            Debug.Log("ID: " + usuarioLogado.nomeID);
            Debug.Log("Nome: " + usuarioLogado.nome);
            Debug.Log("Email: " + usuarioLogado.email);

            if (txtNomeUsuario != null)
                txtNomeUsuario.text = "Usuário: " + usuarioLogado.nome;

            if (txtEmailUsuario != null)
                txtEmailUsuario.text = usuarioLogado.email;
        }
        else
        {
            Debug.LogError("Erro ao desserializar UsuarioData.");
        }
    }

    public void AtualizarUI()
    {
        if (AcertosErrosController.Instance != null)
        {
            txtAcertos.text = "Acertos: " + AcertosErrosController.Instance.acertos;
            txtErros.text = "Erros: " + AcertosErrosController.Instance.erros;
            txtPontuacao.text = "Pontuação: " + AcertosErrosController.Instance.pontuacao;
        }
        else
        {
            txtAcertos.text = "Acertos: 0";
            txtErros.text = "Erros: 0";
            txtPontuacao.text = "Pontuação: 0";
        }

        int nomeID = PlayerPrefs.GetInt("nomeID", 0);
        if (nomeID == 0)
        {
            botaoEnviar.interactable = false;
            if (txtLoginAviso != null) txtLoginAviso.gameObject.SetActive(true);
        }
        else
        {
            botaoEnviar.interactable = true;
            if (txtLoginAviso != null) txtLoginAviso.gameObject.SetActive(false);
        }
    }
}
