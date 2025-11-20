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
    // Få tak i om vi traff en circle collider
    if (collision is CircleCollider2D)
    {
        // sjekk om det er BottomMine
        if (collision.transform.CompareTag("BottomMine"))
        {
            collision.GetComponent<BottomMineHit>()?.Explode();
            Destroy(gameObject);
            return;
        }

        // sjekk om det er TopMine
        if (collision.transform.CompareTag("TopMine"))
        {
            collision.GetComponent<TopMineHit>()?.Explode();
            Destroy(gameObject);
            return;
        }
    }

    // Treffer vi kjeden? Ikke gjør noe!
}


}
