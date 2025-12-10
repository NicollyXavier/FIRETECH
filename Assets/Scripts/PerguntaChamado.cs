using UnityEngine;
using UnityEngine.SceneManagement;

public class PerguntaChamado : MonoBehaviour
{
    public void AceitarChamado()
    {
        PlayerPrefs.SetString("VideoToPlay", "video2_caminhao.mp4");
        PlayerPrefs.SetString("NextScene", "Cena_1_Externa");
        SceneManager.LoadScene("Cena_Video");
    }

    public void RecusarChamado()
    {
        SceneManager.LoadScene("Cena_Final_Encerramento");
    }
}
