using UnityEngine;
using UnityEngine.SceneManagement;

public class DecisaoCena2 : MonoBehaviour
{
    public void EscolheuSim()
    {
        // regista acerto
        if (AcertosErrosController.Instance != null)
            AcertosErrosController.Instance.RegistrarAcerto();

        SceneManager.LoadScene("Cena_3_Incendio");
    }

    public void EscolheuNao()
    {
        if (AcertosErrosController.Instance != null)
        AcertosErrosController.Instance.RegistrarErroEEncerrar("Cena_2_Interior");
    }
}
