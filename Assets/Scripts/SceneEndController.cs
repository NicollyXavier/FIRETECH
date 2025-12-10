using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEndController : MonoBehaviour
{
    public void VoltarParaInicio()
    {
        SceneManager.LoadScene("Cena_MenuInicial");
    }
}
