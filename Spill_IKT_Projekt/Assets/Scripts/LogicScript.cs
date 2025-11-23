using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;

    public bool isGameOver = false;

    private int lastSpeedLevel = 0;
    public float mineSpeedIncrease = 2f;

    private MineSpawnerScript spawner;

    void Start()
    {
        spawner = GameObject.FindObjectOfType<MineSpawnerScript>();
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
        // Øk farten på nye miner
        if (spawner != null)
        {
            spawner.currentMineSpeed += mineSpeedIncrease;
        }

        // Øk farten på eksisterende miner
        MineMoveScript[] allMines =
            FindObjectsByType<MineMoveScript>(FindObjectsSortMode.None);

        foreach (MineMoveScript mine in allMines)
        {
            mine.moveSpeed += mineSpeedIncrease;
        }

        Debug.Log("Fart økt med +" + mineSpeedIncrease);
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
