using UnityEngine;

public class Parallax : MonoBehaviour
{
    Material mat;
    float distance;

    [Range(0f, 0.5f)]
    public float speed = 0.2f;

    private LogicScript logic;

    void Start()
    {
        mat = GetComponent<Renderer>().material;

        // finn LogicScript (bruker tag “Logic” fra spillet ditt)
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        // ❄️ Stop scrolling når game over
        if (logic != null && logic.IsFrozen())
            return;

        distance += Time.deltaTime * speed;
        mat.SetTextureOffset("_MainTex", Vector2.right * distance);
    }
}
