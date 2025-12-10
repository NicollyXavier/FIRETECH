using UnityEngine;
using UnityEngine.SceneManagement;

public class DecisaoResgate : MonoBehaviour
{
    public void EscolheuSim()
    {
        // regista acerto
        if (AcertosErrosController.Instance != null)
            AcertosErrosController.Instance.RegistrarAcerto();

        PlayerPrefs.SetString("VideoToPlay", "video5_resgate.mp4");
        PlayerPrefs.SetString("NextScene", "Cena_Final_Encerramento");
        SceneManager.LoadScene("Cena_Video");
    }

    public void EscolheuNao()
    {
        if (AcertosErrosController.Instance != null)
        AcertosErrosController.Instance.RegistrarErroEEncerrar("Cena_4_Vitima");
    }
}
