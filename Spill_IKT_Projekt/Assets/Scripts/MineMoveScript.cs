using UnityEngine;

public class MineMoveScript : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadZone = -20;

    private LogicScript logic;

    void Start()
    {
        // Finn LogicScript i scenen
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
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
}
