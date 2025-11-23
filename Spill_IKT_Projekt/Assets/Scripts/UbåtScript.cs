using UnityEngine;

public class UbåtScript : MonoBehaviour
{
    public LogicScript logic;
    public bool ubåtIsAlive = true;

    public Rigidbody2D myRigidbody;
    public float oppKraft = 7f;
    public float maksOppHastighet = 8f;

    [Header("Skyting")]
    public GameObject torpedoPrefab;
    public Transform firePoint;
    public float shootCooldown = 0.5f;
    private float shootTimer = 0f;

    [Header("Animation")]
    public Animator myAnimator;              // svømmeanimasjonen
    public GameObject explosionPrefab;       // eksplosjon (samme som minene)

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

        ubåtIsAlive = false;

        // Spill eksplosjonsanimasjon (hvis du senere bruker anim)
        if (myAnimator != null)
        {
            myAnimator.SetTrigger("Explode");
        }

        // Spawn eksplosjon som prefab (samme som minene)
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // Skru av sprite på ubåten
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;

        // Stopp all bevegelse umiddelbart
        myRigidbody.linearVelocity = Vector2.zero;
        myRigidbody.linearVelocity = Vector2.zero;

        // ❄️ slå av gravity så ubåten IKKE begynner å synke
        myRigidbody.gravityScale = 0f;

        // Slå av collider så den ikke kolliderer igjen
        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // Aktiver Game Over (soft freeze)
        logic.gameOver();
    }
}
