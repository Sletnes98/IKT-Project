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
    // 1️⃣ Hvis ikke CircleCollider2D → IGNORER treffet
    if (!(collision is CircleCollider2D))
        return;

    // 2️⃣ Hvis objektet har MineHealth → bruk health-systemet
    MineHealth health = collision.GetComponent<MineHealth>();
    if (health != null)
    {
        health.TakeDamage();
        Destroy(gameObject);
        return;
    }

    // 3️⃣ Ellers: sjekk TopMine og BottomMine
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

    // Treffer du andre ting → gjør ingenting
}

}
