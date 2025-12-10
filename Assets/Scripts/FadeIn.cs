using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public float duration = 1.5f;
    private Image img;
    private float timer = 0f;

    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(0, 0, 0, 1);
    }

    void Update()
    {
        if (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = 1 - (timer / duration);
            img.color = new Color(0, 0, 0, alpha);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
