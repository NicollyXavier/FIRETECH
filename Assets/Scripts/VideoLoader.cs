using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoLoader : MonoBehaviour
{
    public VideoPlayer player;

    void Start()
    {
        string videoFile = PlayerPrefs.GetString("VideoToPlay", "");
        string nextScene = PlayerPrefs.GetString("NextScene", "");

        if (string.IsNullOrEmpty(videoFile))
        {
            Debug.LogError("Nenhum vídeo definido! Verifique PlayerPrefs.");
            return;
        }

        // Caminho do vídeo
        player.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoFile);

        // Quando terminar de preparar, toca
        player.prepareCompleted += (vp) => player.Play();

        // Ao terminar o vídeo, vai para a próxima cena
        player.loopPointReached += (vp) =>
        {
           
           if (videoFile == "video5_resgate.mp4")
        {
        if (AcertosErrosController.Instance != null)
            AcertosErrosController.Instance.MarcarConcluiu();
        }

            if (!string.IsNullOrEmpty(nextScene))
                SceneManager.LoadScene(nextScene);
        };

        player.Prepare();
    }
}
