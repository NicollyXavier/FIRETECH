using UnityEngine;
using UnityEngine.SceneManagement;

public class DecisaoCena3 : MonoBehaviour
{
    public void EscolheuSim()
    {
        // regista acerto
        if (AcertosErrosController.Instance != null)
            AcertosErrosController.Instance.RegistrarAcerto();

        PlayerPrefs.SetString("VideoToPlay", "video4_apagando.mp4");
        PlayerPrefs.SetString("NextScene", "Cena_4_Vitima");
        SceneManager.LoadScene("Cena_Video");
    }

    public void EscolheuNao()
    {
        if (AcertosErrosController.Instance != null)
        AcertosErrosController.Instance.RegistrarErroEEncerrar("Cena_3_Incendio");
    }
}
