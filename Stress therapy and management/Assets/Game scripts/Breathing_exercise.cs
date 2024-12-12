using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using System;

public class Breathing_exercise : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text phaseText;
    public TMP_Text remainingTimeText;
    public Button button;
    //Regulate_timer time;

    private float timer;
    private int phase;
    private float totalTime;
    private int breathing_time = 0;
    private float maxTime;
    private bool isTimerRunning = true;
    private float breathingExerciseTimer = 0f; // Tracks elapsed breathing time in seconds
    private int totalBreathingTimeToday = 0; // Tracks total breathing time in minutes
    private DateTime lastUpdate;

    public InputActionProperty stopstarttimer;
    private const string BreathingTimeKey = "TotalBreathingTime";
    private const string LastUpdateKey = "LastUpdateDate";
    public Carry_forward_time timer_breathe;
    private void Start()
    {
        timer_breathe = FindObjectOfType<Carry_forward_time>();
        breathing_time = timer_breathe.breathing_timer_set;
        totalTime = 0;
        maxTime = breathing_time * 60;

        string lastUpdateString = PlayerPrefs.GetString(LastUpdateKey, DateTime.MinValue.ToString());
        lastUpdate = DateTime.Parse(lastUpdateString);

        if (DateTime.Now.Date > lastUpdate.Date)
        {
            totalBreathingTimeToday = 0;
        }
        else
        {
            totalBreathingTimeToday = PlayerPrefs.GetInt(BreathingTimeKey, 0);
        }

        StartBreathingCycle();
    }

    private void Update()
    {
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
            breathingExerciseTimer += Time.deltaTime;

            if (breathingExerciseTimer >= 60f)
            {
                breathingExerciseTimer -= 60f;
                totalBreathingTimeToday += 1;
                PlayerPrefs.SetInt(BreathingTimeKey, totalBreathingTimeToday);
                timer_breathe.total_breathing_time = totalBreathingTimeToday;
                //time_update.Breathing_time = totalBreathingTimeToday;
            }

            UpdateRemainingTimeDisplay();
        }

        HandleControllerInputs();

        if (totalTime >= maxTime && isTimerRunning)
        {
            EndBreathingExercise();
        }
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
            case 0:
                SetPhase(7, "Hold");
                phase = 1;
                break;
            case 1:
                SetPhase(8, "Exhale");
                phase = 2;
                break;
            case 2:
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
        timerText.text = Mathf.Ceil(timer).ToString();
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

    private void OnDestroy()
    {
        PlayerPrefs.SetInt(BreathingTimeKey, totalBreathingTimeToday);
        PlayerPrefs.SetString(LastUpdateKey, DateTime.Now.ToString());
    }
}