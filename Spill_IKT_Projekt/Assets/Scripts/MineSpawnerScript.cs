using UnityEngine;

public class MineSpawnerScript : MonoBehaviour
{
    [Header("Mine Prefabs")]
    public GameObject mineOriginal;
    public GameObject mineBlocked;

    [Header("Spawn Settings")]
    public float spawnRate = 2f;
    private float timer = 0f;

    [Header("Spawn Area")]
    public float minHeight = -2f;
    public float maxHeight = 2f;
    public float xOffset = 12f;

    [Range(0f, 1f)]
    public float blockedChance = 0.2f;  // 20% som standard

    void Start()
    {
        SpawnMine();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnMine();
            timer = 0f;
        }
    }

    void SpawnMine()
    {
        float randomY = Random.Range(minHeight, maxHeight);
        float spawnX = transform.position.x + xOffset;
        Vector3 spawnPos = new Vector3(spawnX, randomY, 0);

        // 80% original, 20% blocked
        if (Random.value < blockedChance)
        {
            Instantiate(mineBlocked, spawnPos, Quaternion.identity);
        }
        else
        {
            Instantiate(mineOriginal, spawnPos, Quaternion.identity);
        }
    }
}
