using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;

    public bool isGameOver = false;

    // 🚀 Hvor mange ganger vi har økt farten
    private int lastSpeedLevel = 0;

    // 🚀 Hvor mye farten øker hver gang
    public float mineSpeedIncrease = 2f;

    private MineSpawnerScript spawner;

    void Start()
    {
        // Finn spawner i scenen
        spawner = GameObject.FindObjectOfType<MineSpawnerScript>();
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd = 1)
    {
        if (isGameOver) return;

        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();

        // 🔥 Øk fart hver 5 poeng (tidligere 10)
        int speedLevel = playerScore / 5;

        if (speedLevel > lastSpeedLevel)
        {
            lastSpeedLevel = speedLevel;
            IncreaseMineSpeed();
        }
    }

    void IncreaseMineSpeed()
    {
        // --- Øk farten for NYE miner ---
        if (spawner != null)
        {
            spawner.currentMineSpeed += mineSpeedIncrease;
        }

        // --- Øk farten for ALLE miner som finnes nå ---
        MineMoveScript[] allMines =
            FindObjectsByType<MineMoveScript>(FindObjectsSortMode.None);

        foreach (MineMoveScript mine in allMines)
        {
            mine.moveSpeed += mineSpeedIncrease;
            mine.RefreshAnimatorSpeed();   // 🔥 Oppdater animasjonshastighet
        }

        Debug.Log("🔥 Fart økt! Nye speed: +" + mineSpeedIncrease);
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

    // Brukes av mine scripts for å stoppe bevegelse på game over
    public bool IsFrozen()
    {
        return isGameOver;
    }
}
