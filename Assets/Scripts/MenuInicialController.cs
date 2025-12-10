using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuInicialController : MonoBehaviour
{
    [Header("UI")]

    private UsuarioData usuarioLogado;

    void Start()
    {
        CarregarUsuario();
    }

    public void IniciarSimulacao()
    {
        SceneManager.LoadScene("Cena_0_Chamado");
    }

     public void IrParaPlacar()
    {
        SceneManager.LoadScene("Placar");
    }

    public void Sair()
    {
        Application.Quit();
        Debug.Log("Aplicação encerrada.");
    }

    void CarregarUsuario()
    {
        string json = PlayerPrefs.GetString("usuario_json", "");

        if (string.IsNullOrEmpty(json))
        {
            Debug.LogWarning("Nenhum usuário logado encontrado no PlayerPrefs.");
            return;
        }

        usuarioLogado = JsonUtility.FromJson<UsuarioData>(json);

        if (usuarioLogado != null)
        {
            Debug.Log("Usuário logado:");
            Debug.Log(json);

            Debug.Log("Bem-vindo, " + usuarioLogado.nome + "!");
            Debug.Log("Bem-vindo, " + usuarioLogado.nomeID + "!");
        }
        else
        {
            Debug.LogError("Erro ao desserializar UsuarioData.");
             Debug.Log("Bem-vindo!");
        }
    }
}