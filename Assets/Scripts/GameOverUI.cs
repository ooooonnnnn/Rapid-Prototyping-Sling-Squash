using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Timer timer;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private Button restartButton;

    private void Awake()
    {
        gameOverPanel.SetActive(false);
    }

    private void OnEnable()
    {
        if (timer != null) timer.OnTimeUp += HandleTimeUp;
        if (restartButton != null) restartButton.onClick.AddListener(RestartGame);
    }

    private void OnDisable()
    {
        if (timer != null) timer.OnTimeUp -= HandleTimeUp;
        if (restartButton != null) restartButton.onClick.RemoveListener(RestartGame);
    }

    private void HandleTimeUp()
    {
        if (finalScoreText != null)
            finalScoreText.text = "Score: " + ScoreManager.Instance.GetScore();

        gameOverPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // unpause and reload scene
        Time.timeScale = 1f;
        Scene current = SceneManager.GetActiveScene();
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(current.buildIndex);
    }
}
