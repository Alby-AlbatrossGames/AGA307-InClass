using UnityEngine;

public enum GameState
{
    Ready,
    Playing,
    Paused,
    GameOver
}

public enum Difficulty
{
    Chill,
    Regular,
    Ayo
}

public class GameManager : MonoBehaviour
{
    public GameState gameState;
    public Difficulty difficulty;
    public int score;
    private int scoreMultiplier = 1;

    void Start()
    {
        switch (difficulty)
        {
            case Difficulty.Chill:   scoreMultiplier = 1; break;
            case Difficulty.Regular: scoreMultiplier = 2; break;
            case Difficulty.Ayo:     scoreMultiplier = 3; break;
        }
    }
}
