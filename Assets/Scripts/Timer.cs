using System;
using TMPro;
using UnityEngine;
public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float startTime = 60f; // seconds

    public event Action OnTimeUp;
    
    private float currentTime;
    private bool isRunning = true;

    private void Start()
    {
        currentTime = startTime;
        UpdateTimerUI();
    }

    private void Update()
    {
        if (!isRunning) return;

        currentTime -= Time.deltaTime;
        if (currentTime <= 0f)
        {
            currentTime = 0f;
            isRunning = false;
            UpdateTimerUI();
            OnTimeUp?.Invoke();
            return;
        }
        
        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    public void ResetTimer(float newTime)
    {
        currentTime = newTime;
        isRunning = true;
        UpdateTimerUI();
    }

    public float GetTime() => currentTime;
}
