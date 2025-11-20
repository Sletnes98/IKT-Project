using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float speed = 0.1f;       // Sett forskjellig verdi per lag
    private Material mat;
    private Vector2 offset;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        offset = mat.mainTextureOffset;
    }

    void Update()
    {
        offset.x += speed * Time.deltaTime;
        mat.mainTextureOffset = offset;
    }
}
