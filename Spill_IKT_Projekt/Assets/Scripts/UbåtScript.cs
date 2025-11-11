using UnityEngine;

public class UbåtScript : MonoBehaviour
{
    public LogicScript logic;
    public bool ubåtIsAlive = true;
    public Rigidbody2D myRigidbody;
    public float oppKraft = 7f;
    public float maksOppHastighet = 8f;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) == true && ubåtIsAlive == true)
        {
            // Legg til kraft oppover så lenge vi ikke går for fort
            if (myRigidbody.linearVelocity.y < maksOppHastighet)
            {
                myRigidbody.AddForce(Vector2.up * oppKraft);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.gameOver();
        ubåtIsAlive  = false;
    }
}
