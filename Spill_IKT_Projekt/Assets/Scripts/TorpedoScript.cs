using UnityEngine;

public class TorpedoScript : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;

    public AudioClip hitSound;   // 🔊 Dra inn lyden her
    public float soundVolume = 1f;

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
        // Ignorér alt som ikke er CircleCollider
        if (!(collision is CircleCollider2D))
            return;

        //  Spill lyd der torpedoen traff
        if (hitSound != null)
            AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position, soundVolume);


        // Treff mine med health
        MineHealth health = collision.GetComponent<MineHealth>();
        if (health != null)
        {
            health.TakeDamage();
            Destroy(gameObject);
            return;
        }

        // Treff Top Mine
        if (collision.CompareTag("TopMine"))
        {
            collision.GetComponent<TopMineHit>()?.Explode();
            Destroy(gameObject);
            return;
        }

        // Treff Bottom Mine
        if (collision.CompareTag("BottomMine"))
        {
            collision.GetComponent<BottomMineHit>()?.Explode();
            Destroy(gameObject);
            return;
        }

        // Standard sletting hvis noe annet
        Destroy(gameObject);
    }
}
