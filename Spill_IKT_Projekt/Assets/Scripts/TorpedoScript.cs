using UnityEngine;

public class TorpedoScript : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f; // hvor lenge torpedoen lever før den forsvinner

    void Start()
    {
        // Ødelegg torpedoen etter noen sekunder så den ikke fyller scenen
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Få torpedoen til å fly fremover
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mine"))
        {
            // Her kan du legge til eksplosjon senere 💥
            Destroy(collision.gameObject); // Ødelegg minen
            Destroy(gameObject); // Ødelegg torpedoen
        }
    }
}
