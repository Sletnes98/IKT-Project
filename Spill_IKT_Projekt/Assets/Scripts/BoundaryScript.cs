using UnityEngine;

public class BoundaryScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ubåt"))
        {
            // La ubåtens egen dødsmekanisme ta seg av alt
            collision.GetComponent<UbåtScript>().Die();
        }
    }
}
