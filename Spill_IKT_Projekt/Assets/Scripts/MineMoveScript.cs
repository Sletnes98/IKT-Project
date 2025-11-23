using UnityEngine;

public class MineMoveScript : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadZone = -20;

    private LogicScript logic;
    private Animator anim;

    void Start()
    {
        // Finn LogicScript i scenen
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

        // Finn Animator hvis minen har en
        anim = GetComponent<Animator>();

        // Juster animasjonshastighet basert på moveSpeed
        if (anim != null)
        {
            anim.speed = moveSpeed / 5f; 
            // 5f er "base speed" – juster opp/ned om du vil ha mer eller mindre dramatisk effekt
        }
    }

    void Update()
    {
        // ❄️ Soft freeze: stopp bevegelse når gameOver = true
        if (logic != null && logic.IsFrozen())
            return;

        // Flytt minen mot venstre
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // Fjern minen hvis den går utenfor skjermen
        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }

    // Kall denne når LogicScript øker farten (brukes inne i IncreaseMineSpeed())
    public void RefreshAnimatorSpeed()
    {
        if (anim != null)
        {
            anim.speed = moveSpeed / 5f;
        }
    }
}
