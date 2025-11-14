using UnityEngine;

public class UbåtScript : MonoBehaviour
{
    public LogicScript logic;
    public bool ubåtIsAlive = true;
    public Rigidbody2D myRigidbody;
    public float oppKraft = 7f;
    public float maksOppHastighet = 8f;

    [Header("Skyting")]
    public GameObject torpedoPrefab;      // Prefab du laget
    public Transform firePoint;           // Tomt objekt foran ubåten
    public KeyCode shootKey = KeyCode.LeftControl; // Bytt tast om du vil
    public float shootCooldown = 0.5f;    // Hvor ofte man kan skyte
    private float shootTimer = 0f;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        if (!ubåtIsAlive) return;

        // 🫧 Flyt opp med SPACE
        if (Input.GetKey(KeyCode.Space))
        {
            if (myRigidbody.linearVelocity.y < maksOppHastighet)
            {
                myRigidbody.AddForce(Vector2.up * oppKraft);
            }
        }

        // 🔫 Skyte med LeftControl (eller annen tast)
        shootTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && shootTimer >= shootCooldown)
{
    ShootTorpedo();
    shootTimer = 0f;
}

    }

    void ShootTorpedo()
    {
        Instantiate(torpedoPrefab, firePoint.position, firePoint.rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.gameOver();
        ubåtIsAlive = false;
    }
}

