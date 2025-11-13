using UnityEngine;

public class BoundaryScript : MonoBehaviour
{
    private LogicScript logic;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.CompareTag("Ubåt"))
    {
        logic.gameOver();

        // hent ubåt-skriptet og slå av styringen
        collision.gameObject.GetComponent<UbåtScript>().ubåtIsAlive = false;
    }
}

}
