using UnityEngine;

public class MineMoveScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float deadZone = -20f;

    private LogicScript logic;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        // ❄️ stopp bevegelse på game over
        if (logic != null && logic.IsFrozen())
            return;

        // flytt minen
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // slett når den går ut av skjermen
        if (transform.position.x < deadZone)
            Destroy(gameObject);
    }
}
