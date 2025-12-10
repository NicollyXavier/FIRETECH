using UnityEngine;
using UnityEngine.Video;

public class ChamadoVideoLoader : MonoBehaviour
{
    public VideoPlayer player;

    void Start()
    {
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, "video1_chamado.mp4");

        player.url = path;

        player.prepareCompleted += (vp) => player.Play();
        player.Prepare();
    }
}
