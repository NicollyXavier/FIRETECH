using UnityEngine;
using UnityEngine.SceneManagement;

public class ChamadoController : MonoBehaviour
{
    public void AceitarChamado()
    {
        // regista acerto
        if (AcertosErrosController.Instance != null)
            AcertosErrosController.Instance.RegistrarAcerto();

        PlayerPrefs.SetString("VideoToPlay", "video2_caminhao.mp4");
        PlayerPrefs.SetString("NextScene", "Cena_1_Externa");
        SceneManager.LoadScene("Cena_Video");
    }

    public void RecusarChamado()
    {
        if (AcertosErrosController.Instance != null)
        AcertosErrosController.Instance.RegistrarErroEEncerrar("Cena_0_Chamado");
    }
}
