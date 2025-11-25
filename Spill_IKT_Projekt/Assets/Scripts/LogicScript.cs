using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;

    public GameObject gameOverScreen;
    public GameObject playButton;   // ⬅ Play-knappen i Canvas

    public bool isGameOver = false;
    public bool gameStarted = false; // ⬅ Hindrer spillet før du trykker Play

    private int lastSpeedLevel = 0;

    public float mineSpeedIncrease = 2f;

    private MineSpawnerScript spawner;

    void Start()
{
    spawner = FindFirstObjectByType<MineSpawnerScript>();

    gameStarted = false;
    playButton.SetActive(true);

    // 🚤 Skru av fysikken på ubåten i starten
    Rigidbody2D rb = GameObject.FindGameObjectWithTag("Ubåt").GetComponent<Rigidbody2D>();
    rb.simulated = false;
}


    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd = 1)
    {
        if (isGameOver || !gameStarted) return;

        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();

        int speedLevel = playerScore / 5;

        if (speedLevel > lastSpeedLevel)
        {
            lastSpeedLevel = speedLevel;
            IncreaseMineSpeed();
        }
    }

    void IncreaseMineSpeed()
    {
        // Øk haste for nye miner
        if (spawner != null)
            spawner.currentMineSpeed += mineSpeedIncrease;

        // Øk farten for eksisterende miner
        MineMoveScript[] allMines =
            FindObjectsByType<MineMoveScript>(FindObjectsSortMode.None);

        foreach (MineMoveScript mine in allMines)
            mine.moveSpeed += mineSpeedIncrease;

        // Øk farten for parallax
        Parallax[] layers =
            FindObjectsByType<Parallax>(FindObjectsSortMode.None);

        foreach (Parallax p in layers)
            p.IncreaseParallax();

        Debug.Log("Fart økt for miner og parallax!");
    }

    public void StartGame()
{
    gameStarted = true;
    playButton.SetActive(false);

    // 🚤 Slå på fysikk når spillet starter
    Rigidbody2D rb = GameObject.FindGameObjectWithTag("Ubåt").GetComponent<Rigidbody2D>();
    rb.simulated = true;

    Debug.Log("GAME STARTED!");
}


    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        isGameOver = true;
        gameOverScreen.SetActive(true);
        Debug.Log("Game Over!");
    }

    // Alt stopper hvis spillet ikke har startet ELLER hvis GameOver
    public bool IsFrozen()
    {
        return isGameOver || !gameStarted;
    }
}
