using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    [SerializeField] private TextMeshProUGUI scoreText;
    private int highScore;
    [SerializeField] private int devScore;

    private int score = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        devScore = PlayerPrefs.GetInt("DevScore", 0);
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        UpdateScoreUI();
    }

    public int GetScore()
    {
        return score;
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score + "\n" + 
                "High Score: " + highScore + "\n" + 
                "Developer's Record: " + devScore;
        }
    }

    [SerializeField] private int inputDevScore;
    public void SetDevScore()
    {
        PlayerPrefs.SetInt("DevScore", inputDevScore);
    }
}
