using UnityEngine;

public class UbåtScript : MonoBehaviour
{
    public LogicScript logic;
    public bool ubåtIsAlive = true;

    [Header("Bevegelse")]
    public Rigidbody2D myRigidbody;
    public float oppKraft = 7f;
    public float maksOppHastighet = 8f;

    [Header("Skyting")]
    public GameObject torpedoPrefab;
    public Transform firePoint;
    public float shootCooldown = 0.5f;
    private float shootTimer = 0f;

    [Header("Animation & eksplosjon")]
    public Animator myAnimator;            // svømmeanimasjon (propell osv.)
    public GameObject explosionPrefab;     // eksplosjon-prefab (samme type som minene kan bruke)

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        // Stopp all input hvis ubåten er død
        if (!ubåtIsAlive) return;

        // 🫧 Flyt opp med SPACE
        if (Input.GetKey(KeyCode.Space))
        {
            if (myRigidbody.linearVelocity.y < maksOppHastighet)
            {
                myRigidbody.AddForce(Vector2.up * oppKraft);
            }
        }

        // 🔫 Skyte med venstre musetast
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
        if (!ubåtIsAlive) return;

        // 👉 Gi minen 2 damage hvis den har MineHealth
        MineHealth mine = collision.collider.GetComponent<MineHealth>();
        if (mine != null)
        {
            mine.TakeDamage(); // 1 damage
            mine.TakeDamage(); // 2 damage totalt fra krasj
        }

        ubåtIsAlive = false;

        // Stopp svømmeanimasjon (valgfritt)
        if (myAnimator != null)
        {
            myAnimator.enabled = false;
        }

        // Spawn eksplosjon ved ubåten
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // Skru av sprite på ubåten
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;

        // Slå av collider så den ikke kolliderer flere ganger
        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // Stopp all bevegelse og tyngdekraft
        myRigidbody.linearVelocity = Vector2.zero;
        myRigidbody.linearVelocity = Vector2.zero;
        myRigidbody.gravityScale = 0f;

        // Game over (soft freeze – resten stopper via IsFrozen())
        logic.gameOver();
    }
}
