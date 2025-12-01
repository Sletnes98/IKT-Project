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
    public Animator myAnimator;
    public GameObject explosionPrefab;

    private bool spaceHeld = false;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        if (!ubåtIsAlive) return;

        // Les input HER
        spaceHeld = Input.GetKey(KeyCode.Space);

        // 🔫 Skyting
        shootTimer += Time.deltaTime;
        if (logic.gameStarted && Input.GetMouseButtonDown(0) && shootTimer >= shootCooldown)
        {
            ShootTorpedo();
            shootTimer = 0f;
        }
    }

    void FixedUpdate()
    {
        if (!ubåtIsAlive) return;

        if (spaceHeld)
        {
            // BRUK linearVelocity
            if (myRigidbody.linearVelocity.y < maksOppHastighet)
            {
                myRigidbody.AddForce(Vector2.up * oppKraft, ForceMode2D.Force);
            }
        }
    }

    void ShootTorpedo()
    {
        var src = GetComponent<AudioSource>();
        src.pitch = Random.Range(0.9f, 1.1f);
        src.Play();

        Instantiate(torpedoPrefab, firePoint.position, firePoint.rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MineHealth mine = collision.collider.GetComponent<MineHealth>();
        if (mine != null)
        {
            mine.TakeDamage();
            mine.TakeDamage(); 
        }

        Die();
    }

    public void Die()
    {
        if (!ubåtIsAlive) return;
        ubåtIsAlive = false;

        if (myAnimator != null)
            myAnimator.enabled = false;

        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;

        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // Bruk linearVelocity
        myRigidbody.linearVelocity = Vector2.zero;
        myRigidbody.gravityScale = 0f;

        CameraShake.instance.Shake(0.4f, 0.3f);

        logic.gameOver();
    }
}
