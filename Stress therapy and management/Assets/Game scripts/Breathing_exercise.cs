using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Breathing_exercise : MonoBehaviour
{
    public InputField timerText; // UI Text to display the timer
    public InputField phaseText; // UI Text to display the current phase
    public InputField remainingTimeText; // UI Text to display remaining time for the entire exercise
    public Button button; // Optional button for other interactions

    private float timer; // Timer for the current phase
    private int phase; // 0 = Inhale, 1 = Hold, 2 = Exhale
    private float totalTime; // Total elapsed time
    public int breathing_time = 20; // Total breathing exercise duration in minutes
    private float maxTime; // Maximum time for the exercise (20 minutes)
    private bool isTimerRunning = true; // Control for pausing the timer

    private void Start()
    {
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

        // Handle Controller Inputs
        HandleControllerInputs();
    }

    private void StartBreathingCycle()
    {
        phase = 0; // Start with Inhaling
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
            remainingTimeText.text = $"Remaining: {minutes:00}:{seconds:00}";
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
        // Check Left Controller Primary Button (A) to stop/start timer
        if (Input.GetButtonDown("XRI_Left_PrimaryButton"))
        {
            isTimerRunning = !isTimerRunning; // Toggle the timer state
        }

        // Check Left Controller Secondary Button (B) to go to the previous scene
        if (Input.GetButtonDown("XRI_Left_SecondaryButton"))
        {
            GoToPreviousScene();
        }
    }

    private void GoToPreviousScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex > 0)
        {
            SceneManager.LoadScene(currentSceneIndex - 1); // Load the previous scene
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
