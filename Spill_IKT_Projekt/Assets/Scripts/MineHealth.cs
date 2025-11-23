using UnityEngine;

public class MineHealth : MonoBehaviour
{
    public GameObject explosionPrefab;
    public int health = 1;

    [Header("Chain neighbors")]
    public MineHealth neighborAbove;
    public MineHealth neighborBelow;

    bool isExploding = false;

    public void TakeDamage()
    {
        TakeDamage(true);
    }

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

        GameObject exp = null;

        // Spawn eksplosjon
        if (explosionPrefab != null)
            exp = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // ⭐ FINN moveSpeed FRA PARENT SOM HAR MineMoveScript ⭐
        MineMoveScript parentMove = GetComponentInParent<MineMoveScript>();

        if (exp != null)
        {
            MineMoveScript expMove = exp.GetComponent<MineMoveScript>();

            if (expMove != null && parentMove != null)
                expMove.moveSpeed = parentMove.moveSpeed;
        }

        // chain reaction
        if (doChain)
            DamageNeighbors();

        // Skjul sprite og collider
        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;

        Destroy(gameObject, 0.05f);
    }

    void DamageNeighbors()
    {
        if (neighborAbove != null)
            neighborAbove.TakeDamage(false);

        if (neighborBelow != null)
            neighborBelow.TakeDamage(false);
    }
}
