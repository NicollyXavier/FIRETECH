using UnityEngine;
using UnityEngine.UI;

public class GazeButton : MonoBehaviour
{
    public Image fillBar;
    public float gazeTime = 2f;
    private float timer;
    private bool isGazing = false;

    public UnityEngine.Events.UnityEvent OnGazeComplete;

    void Update()
    {
        if (isGazing)
        {
            timer += Time.deltaTime;
            fillBar.fillAmount = timer / gazeTime;

            if (timer >= gazeTime)
            {
                OnGazeComplete.Invoke();
                ResetGaze();
            }
        }
    }

    public void StartGaze()
    {
        isGazing = true;
        timer = 0;
    }

    public void EndGaze()
    {
        ResetGaze();
    }

    private void ResetGaze()
    {
        isGazing = false;
        timer = 0;
        fillBar.fillAmount = 0;
    }
}
