using UnityEngine;

public class MineSpawnerScript : MonoBehaviour
{
    [Header("Mine Settings")]
    public GameObject mine;
    public float spawnRate = 2f;      // tid mellom spawns
    private float timer = 0f;

    [Header("Spawn Area")]
    public float minHeight = -2f;     // nederste høyde for spawn
    public float maxHeight = 2f;      // høyeste høyde for spawn
    public float xOffset = 12f;       // hvor langt utenfor kamera minene spawner

    void Start()
    {
        spawnMine();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            spawnMine();
            timer = 0f;
        }
    }

    void spawnMine()
    {
        // Bruker faste verdier i stedet for offset fra spawner-posisjonen
        float randomY = Random.Range(minHeight, maxHeight);
        float spawnX = transform.position.x + xOffset; // spawner utenfor kamera til høyre
        Vector3 spawnPos = new Vector3(spawnX, randomY, 0);

        Instantiate(mine, spawnPos, Quaternion.identity);
    }
}
