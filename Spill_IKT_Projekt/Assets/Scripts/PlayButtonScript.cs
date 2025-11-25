using UnityEngine;

public class PlayButtonScript : MonoBehaviour
{
    public LogicScript logic;

    public void PlayButtonPressed()
    {
        if (logic != null)
        {
            logic.StartGame();
        }
        else
        {
            Debug.LogError("PlayButton: No LogicScript assigned!");
        }
    }
}
