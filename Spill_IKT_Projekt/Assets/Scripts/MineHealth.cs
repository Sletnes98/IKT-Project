using UnityEngine;
public class MineHealth : MonoBehaviour
{
    public GameObject explosionPrefab;  // dra inn animasjonen/prefab
    public int health = 1;

    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
