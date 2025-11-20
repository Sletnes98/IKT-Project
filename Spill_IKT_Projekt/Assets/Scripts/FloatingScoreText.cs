using UnityEngine;
using UnityEngine.UI;

public class FloatingScoreText : MonoBehaviour
{
    public float lifetime = 0.5f;
    public float fadeSpeed = 2f;

    private Text text;
    private Color originalColor;

    void Start()
    {
        text = GetComponent<Text>();
        originalColor = text.color;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        originalColor.a -= fadeSpeed * Time.deltaTime;
        text.color = originalColor;
    }
}
