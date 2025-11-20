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
    // sjekk etter BottomMine
    if (collision.CompareTag("BottomMine"))
    {
        collision.GetComponent<BottomMineHit>()?.Explode();
        Destroy(gameObject);
        return;
    }

    // sjekk etter TopMine
    if (collision.CompareTag("TopMine"))
    {
        collision.GetComponent<TopMineHit>()?.Explode();
        Destroy(gameObject);
        return;
    }
}

}
