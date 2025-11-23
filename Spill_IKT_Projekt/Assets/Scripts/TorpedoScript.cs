using UnityEngine;

public class TorpedoScript : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // ❗ IKKE stopp torpedoen ved game over
        // Torpedoen skal kunne fortsette å fly

        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Treffer vi ikke CircleCollider → ignorér
        if (!(collision is CircleCollider2D))
            return;

        // Hvis objektet har health → bruk det
        MineHealth health = collision.GetComponent<MineHealth>();
        if (health != null)
        {
            health.TakeDamage();
            Destroy(gameObject);
            return;
        }

        // Top Mine
        if (collision.CompareTag("TopMine"))
        {
            collision.GetComponent<TopMineHit>()?.Explode();
            Destroy(gameObject);
            return;
        }

        // Bottom Mine
        if (collision.CompareTag("BottomMine"))
        {
            collision.GetComponent<BottomMineHit>()?.Explode();
            Destroy(gameObject);
            return;
        }
    }
}
