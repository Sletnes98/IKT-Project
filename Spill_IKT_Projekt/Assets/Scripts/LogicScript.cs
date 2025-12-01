using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;

    // Highscore UI
    public Text highscoreTextStart;      // vises før spill start
    public Text highscoreTextGameOver;   // vises i game over meny
    private int highscore = 0;

    public GameObject gameOverScreen;
    public GameObject playButton;

    public bool isGameOver = false;
    public bool gameStarted = false;

    private int lastSpeedLevel = 0;
    public float mineSpeedIncrease = 2f;

    private MineSpawnerScript spawner;

    // Lyd når man får poeng
    public AudioSource scoreSound;


    // ----------------------------------------------------------
    // START
    // ----------------------------------------------------------
    void Start()
    {
        spawner = FindFirstObjectByType<MineSpawnerScript>();

        // 🎯 Last highscore
        highscore = PlayerPrefs.GetInt("Highscore", 0);

        // 🎯 Vis highscore før spillet starter
        if (highscoreTextStart != null)
            highscoreTextStart.text = "Highscore: " + highscore;

        // ❄ Spill ikke startet
        gameStarted = false;
        playButton.SetActive(true);

        // 🚤 Skru av fysikk på ubåten i starten
        Rigidbody2D rb = GameObject.FindGameObjectWithTag("Ubåt").GetComponent<Rigidbody2D>();
        rb.simulated = false;

        // Skjul game-over highscore (skal kun vises når man dør)
        if (highscoreTextGameOver != null)
            highscoreTextGameOver.gameObject.SetActive(false);
    }


    // ----------------------------------------------------------
    // ADD SCORE
    // ----------------------------------------------------------
    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd = 1)
    {
        if (isGameOver || !gameStarted)
            return;

        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();

        // 🔊 Spill poenglyd
        if (scoreSound != null)
            scoreSound.Play();

        // 💨 Øk fart hver 5 poeng
        int speedLevel = playerScore / 5;
        if (speedLevel > lastSpeedLevel)
        {
            lastSpeedLevel = speedLevel;
            IncreaseMineSpeed();
        }
    }


    // ----------------------------------------------------------
    // ØK FART
    // ----------------------------------------------------------
    void IncreaseMineSpeed()
    {
        // nye miner
        if (spawner != null)
            spawner.currentMineSpeed += mineSpeedIncrease;

        // eksisterende miner
        MineMoveScript[] allMines =
            FindObjectsByType<MineMoveScript>(FindObjectsSortMode.None);

        foreach (MineMoveScript mine in allMines)
            mine.moveSpeed += mineSpeedIncrease;

        // parallax layers
        Parallax[] layers =
            FindObjectsByType<Parallax>(FindObjectsSortMode.None);

        foreach (Parallax p in layers)
            p.IncreaseParallax();

        Debug.Log("Fart økt for miner og parallax!");
    }


    // ----------------------------------------------------------
    // START GAME
    // ----------------------------------------------------------
    public void StartGame()
    {
        gameStarted = true;
        playButton.SetActive(false);

        // 👀 Skjul start-highscore
        if (highscoreTextStart != null)
            highscoreTextStart.gameObject.SetActive(false);

        // 👀 Skjul game-over highscore hvis den var synlig
        if (highscoreTextGameOver != null)
            highscoreTextGameOver.gameObject.SetActive(false);

        // 🚤 Slå på fysikk på ubåten
        Rigidbody2D rb = GameObject.FindGameObjectWithTag("Ubåt").GetComponent<Rigidbody2D>();
        rb.simulated = true;

        Debug.Log("GAME STARTED!");
    }


    // ----------------------------------------------------------
    // GAME OVER
    // ----------------------------------------------------------
    public void gameOver()
    {
        isGameOver = true;
        gameOverScreen.SetActive(true);

        // 🎯 Oppdater highscore hvis du slo den
        if (playerScore > highscore)
        {
            highscore = playerScore;
            PlayerPrefs.SetInt("Highscore", highscore);
            PlayerPrefs.Save();
        }

        // 🎯 Vis highscore på game over skjerm
        if (highscoreTextGameOver != null)
        {
            highscoreTextGameOver.text = "Highscore: " + highscore;
            highscoreTextGameOver.gameObject.SetActive(true);
        }

        Debug.Log("Game Over!");
    }


    // ----------------------------------------------------------
    // RESTART GAME
    // ----------------------------------------------------------
    public void restartGame()
    {
        // skjul highscore tekst før reload
        if (highscoreTextGameOver != null)
            highscoreTextGameOver.gameObject.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // ----------------------------------------------------------
    // FREEZE CHECK
    // ----------------------------------------------------------
    public bool IsFrozen()
    {
        return isGameOver || !gameStarted;
    }
}
