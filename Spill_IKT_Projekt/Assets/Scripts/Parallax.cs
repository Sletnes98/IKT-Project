using UnityEngine;

public class Parallax : MonoBehaviour
{
    Material mat;
    float distance;

    [Range(0f, 0.5f)]
    public float speed = 0.07f;   // din nåværende front-layer-fart

    // Hvor mye parallaxen skal øke per speed-level
    public float parallaxBoostPerLevel = 0.003f;

    private LogicScript logic;

    void Start()
    {
        mat = GetComponent<Renderer>().material;

        // Finn LogicScript
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        // ❄️ Stopp scrolling ved game over
        if (logic != null && logic.IsFrozen())
            return;

        distance += Time.deltaTime * speed;
        mat.SetTextureOffset("_MainTex", Vector2.right * distance);
    }

    // Kalles fra LogicScript
    public void IncreaseParallax()
    {
        speed += parallaxBoostPerLevel;
    }
}
