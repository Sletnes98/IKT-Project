using UnityEngine;

public class MineHealth : MonoBehaviour
{
    public GameObject explosionPrefab;
    public int health = 1;

    [Header("Chain neighbors")]
    public MineHealth neighborAbove;
    public MineHealth neighborBelow;

    bool isExploding = false;

    // Torpedoen kaller denne
    public void TakeDamage()
    {
        TakeDamage(true);   // første treff kan trigge chain
    }

    // intern versjon der vi kan slå av chain videre
    public void TakeDamage(bool doChain)
    {
        if (isExploding) return;

        health--;

        if (health <= 0)
        {
            Explode(doChain);
        }
    }

    void Explode(bool doChain)
    {
        if (isExploding) return;
        isExploding = true;

        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        if (doChain)
        {
            DamageNeighbors();
        }

        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;

        Destroy(gameObject, 0.05f);
    }

    void DamageNeighbors()
    {
        // bare naboene vi har satt manuelt
        if (neighborAbove != null)
        {
            neighborAbove.TakeDamage(false); // false = ikke starte ny chain
        }

        if (neighborBelow != null)
        {
            neighborBelow.TakeDamage(false);
        }
    }
}
