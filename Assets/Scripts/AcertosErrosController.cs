using UnityEngine;
using UnityEngine.SceneManagement;

public class AcertosErrosController : MonoBehaviour
{
    public static AcertosErrosController Instance;

    [Header("Config")]
    public int pontuacaoFinalSeConcluir = 100;

    [HideInInspector] public int acertos = 0;
    [HideInInspector] public int erros = 0;
    [HideInInspector] public int pontuacao = 100;
    [HideInInspector] public bool desistiu = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            ResetAll();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetAll()
    {
        acertos = 0;
        erros = 0;
        pontuacao = pontuacaoFinalSeConcluir;
        desistiu = false;
    }

    // Call when user chooses "SIM"
    public void RegistrarAcerto()
    {
        acertos++;
        Debug.Log($"Acerto registrado. Total acertos: {acertos}");
    }

    // Call when user chooses "NÃO"
    public void RegistrarErroEEncerrar(string motivoCena = "")
    {
        erros++;
        pontuacao = 0;
        desistiu = true;
        Debug.Log($"Usuário desistiu em: {motivoCena}. Erros: {erros}, Pontuação: {pontuacao}");
        SceneManager.LoadScene("Cena_Final_Encerramento");
    }

    // Call when user completed full flow (last video finished)
    public void MarcarConcluiu()
    {
        desistiu = false;
        pontuacao = pontuacaoFinalSeConcluir;
        Debug.Log("Usuário concluiu a simulação. Pontuação = " + pontuacao);
    }
}
