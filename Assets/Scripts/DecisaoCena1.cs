using UnityEngine;
using UnityEngine.SceneManagement;

public class DecisaoCena1 : MonoBehaviour
{
    public void EscolheuSim()
    {
        // regista acerto
        if (AcertosErrosController.Instance != null)
            AcertosErrosController.Instance.RegistrarAcerto();

        PlayerPrefs.SetString("VideoToPlay", "video3_entrada.mp4");
        PlayerPrefs.SetString("NextScene", "Cena_2_Interior");
        SceneManager.LoadScene("Cena_Video");
    }

    public void EscolheuNao()
    {
        if (AcertosErrosController.Instance != null)
            AcertosErrosController.Instance.RegistrarErroEEncerrar("Cena_1_Externa");
        // Não precisa chamar SceneManager, o RegistrarErroEEncerrar já faz.
    }
}
