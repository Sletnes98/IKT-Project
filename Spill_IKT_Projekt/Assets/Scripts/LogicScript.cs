using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;

    public bool isGameOver = false;

    // Hvor mange ganger farten har økt
    private int lastSpeedLevel = 0;

    // Hvor mye minene øker i fart
    public float mineSpeedIncrease = 2f;

    private MineSpawnerScript spawner;

    void Start()
    {
        // Finn spawner
        spawner = FindFirstObjectByType<MineSpawnerScript>();
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd = 1)
    {
        if (isGameOver) return;

        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();

        // Øk fart hver 5 poeng
        int speedLevel = playerScore / 5;

        if (speedLevel > lastSpeedLevel)
        {
            lastSpeedLevel = speedLevel;
            IncreaseMineSpeed();
        }
    }

    void IncreaseMineSpeed()
    {
        // Øk farten for nye miner
        if (spawner != null)
        {
            spawner.currentMineSpeed += mineSpeedIncrease;
        }

        // Øk farten for ALLE miner i scenen
        MineMoveScript[] allMines =
            FindObjectsByType<MineMoveScript>(FindObjectsSortMode.None);

        foreach (MineMoveScript mine in allMines)
        {
            mine.moveSpeed += mineSpeedIncrease;
        }

        // Øk parallax-farten
        Parallax[] layers =
            FindObjectsByType<Parallax>(FindObjectsSortMode.None);

        foreach (Parallax p in layers)
        {
            p.IncreaseParallax();
        }

        Debug.Log("Fart økt for miner og parallax!");
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

    public bool IsFrozen()
    {
        return isGameOver;
    }
}
