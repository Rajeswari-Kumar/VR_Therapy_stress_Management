using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
public class Breathing_exercise : MonoBehaviour
{
    public TMP_Text timerText; 
    public TMP_Text phaseText;
    public TMP_Text remainingTimeText;
    public Button button;
    Regulate_timer time;
    private float timer;
    private int phase; 
    private float totalTime; 
    public int breathing_time = 20;
    private float maxTime;
    private bool isTimerRunning = true;
    public InputActionProperty stopstarttimer;
    private void Start()
    {
        time = FindObjectOfType<Regulate_timer>();
        breathing_time = time.Breathe_timer;
        totalTime = 0;
        maxTime = breathing_time * 60;
        StartBreathingCycle();
    }

    private void Update()
    {
        // Handle Timer Logic
        if (isTimerRunning && totalTime < maxTime)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                AdvancePhase();
            }

            totalTime += Time.deltaTime;
            UpdateRemainingTimeDisplay();
        }

        HandleControllerInputs();
    }

    private void StartBreathingCycle()
    {
        phase = 0; 
        SetPhase(5, "Inhale");
    }

    private void AdvancePhase()
    {
        switch (phase)
        {
            case 0: // Inhale -> Hold
                SetPhase(7, "Hold");
                phase = 1;
                break;
            case 1: // Hold -> Exhale
                SetPhase(8, "Exhale");
                phase = 2;
                break;
            case 2: // Exhale -> Restart Cycle
                StartBreathingCycle();
                break;
        }
    }

    private void SetPhase(float duration, string phaseName)
    {
        timer = duration;
        phaseText.text = phaseName;
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        timerText.text = Mathf.Ceil(timer).ToString(); // Round up to show whole seconds
    }

    private void UpdateRemainingTimeDisplay()
    {
        if (remainingTimeText != null)
        {
            float remainingTime = Mathf.Max(0, maxTime - totalTime);
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            remainingTimeText.text = $" {minutes:00}:{seconds:00}";
        }
    }

    private void EndBreathingExercise()
    {
        phaseText.text = "Exercise Complete";
        timerText.text = "";
        isTimerRunning = false;
    }

    private void HandleControllerInputs()
    {
        // Check Left Controller Secondary Button (B) to stop/start timer
        if (stopstarttimer.action.IsPressed())
        {
            isTimerRunning = !isTimerRunning;
        }
    }

    public void GuideMeditation()
    {
        if (button != null)
        {
            button.onClick.Invoke();
        }
    }
}
