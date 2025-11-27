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
    public Animator myAnimator;            // ubåtens svømmeanimasjon
    public GameObject explosionPrefab;     // eksplosjon som spawner når ubåten dør

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

        // 🔫 Skyting
        shootTimer += Time.deltaTime;
        // Ikke skyte før spillet har startet
        if (!logic.gameStarted) return;

        if (Input.GetMouseButtonDown(0) && shootTimer >= shootCooldown)
        {
            ShootTorpedo();
            shootTimer = 0f;
        }

    }

    void ShootTorpedo()
{
    // 🔊 Spill lyd (forutsatt at AudioSource er på ubåten)
    var src = GetComponent<AudioSource>();
    src.pitch = Random.Range(0.9f, 1.1f);
    src.Play();
 

    Instantiate(torpedoPrefab, firePoint.position, firePoint.rotation);
}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Hvis vi treffer en mine: gi den damage
        MineHealth mine = collision.collider.GetComponent<MineHealth>();
        if (mine != null)
        {
            mine.TakeDamage(); // 1 damage
            mine.TakeDamage(); // 2 damage totalt fra krasj
        }

        // Ubåten dør uansett hva den treffer
        Die();
    }

    // 🔥 DØDSMETODE — alt samlet ett sted
    public void Die()
    {
        if (!ubåtIsAlive) return;

        ubåtIsAlive = false;

        // Stopp svømmeanimasjon
        if (myAnimator != null)
            myAnimator.enabled = false;

        // Spawn eksplosjon
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Skru av sprite renderer
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;

        // Skru av collider så den ikke treffer noe mer
        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // Stopp bevegelse og tyngdekraft
        myRigidbody.linearVelocity = Vector2.zero;
        myRigidbody.linearVelocity = Vector2.zero;
        myRigidbody.gravityScale = 0f;


        CameraShake.instance.Shake(0.4f, 0.3f);


        // Game over (soft freeze)
        logic.gameOver();
    }
}
