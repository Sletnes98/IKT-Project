using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;

    public bool isGameOver = false;

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd = 1)
    {
        if (isGameOver) return;

        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
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
}
