using System;
using TMPro;
using UnityEngine;
public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float startTime = 60f; // seconds

    public event Action OnTimeUp;
    
    private float currentTime;
    private bool isRunning = false;
    private bool hasStarted = false;

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

    public void StartTimer()
    {
        if (hasStarted) return;
        hasStarted = true;
        isRunning = true;
    }

    public void PauseTimer() => isRunning = false;
    public void ResetTimer(float newTime)
    {
        startTime = newTime;
        currentTime = newTime;
        isRunning = false;
        hasStarted = false;
        UpdateTimerUI();
    }

    public void ResetAndStart(float newTime)
    {
        ResetTimer(newTime);
        StartTimer();
    }

    public bool IsRunning => isRunning;
    public bool HasStarted => hasStarted;
    
    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }
    public float GetTime() => currentTime;
}
