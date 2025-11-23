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
    public float blockedChance = 0.2f;  // 20% sjanse

    // 🔥 NYTT: Fart på alle miner (startfarten)
    public float currentMineSpeed = 5f;

    private LogicScript logic;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

        SpawnMine();
    }

    void Update()
    {
        // ❄️ Stopp spawning hvis spillet er over
        if (logic != null && logic.IsFrozen())
            return;

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

        GameObject prefabToSpawn;

        // Vanlige miner vs blokk-miner
        if (Random.value < blockedChance)
        {
            prefabToSpawn = mineBlocked;
        }
        else
        {
            prefabToSpawn = mineOriginal;
        }

        // Lag mine
        GameObject m = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        // ⭐ Sett fart på den nye minen
        MineMoveScript move = m.GetComponent<MineMoveScript>();
        if (move != null)
            move.moveSpeed = currentMineSpeed;
    }
}
