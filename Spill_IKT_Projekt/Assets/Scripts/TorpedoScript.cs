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
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Først: sjekk om objektet har MineHealth
        MineHealth health = collision.GetComponent<MineHealth>();
        if (health != null)
        {
            health.TakeDamage(); // mister 1 HP
            Destroy(gameObject);// torpedoen forsvinner
            return;
        }

        // Hvis ikke: sjekk TopMine / BottomMine
        if (collision.CompareTag("BottomMine"))
        {
            collision.GetComponent<BottomMineHit>()?.Explode();
            Destroy(gameObject);
            return;
        }

        if (collision.CompareTag("TopMine"))
        {
            collision.GetComponent<TopMineHit>()?.Explode();
            Destroy(gameObject);
            return;
        }
    }
}
