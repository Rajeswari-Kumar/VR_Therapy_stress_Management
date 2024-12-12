using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using TMPro;

public class Guided_meditation : MonoBehaviour
{
    public AudioSource[] audioSource;
    private AudioSource audioSourceToPlay;
    public Slider volumeSlider;
    public InputActionProperty playPauseButton;
    public Slider happiness_slider;
    private bool isPaused = false;
    private float meditationTimer = 0f; // Tracks elapsed time in seconds
    private int totalTimeMeditatedToday = 0; // Tracks total meditation time in minutes
    private DateTime lastUpdate; // Tracks when the meditation time was last updated
    public TMP_Text TotalMeditationtime;
    private const string MeditationTimeKey = "TotalMeditationTime";
    private const string LastUpdateKey = "LastUpdateDate";
    Carry_forward_time timer;
    float happiness_value;
    private void Start()
    {
        timer = FindObjectOfType<Carry_forward_time>();
        happiness_slider.maxValue = timer.meditation_timer_set;
        // Load the last update date
        string lastUpdateString = PlayerPrefs.GetString(LastUpdateKey, DateTime.MinValue.ToString());
        lastUpdate = DateTime.Parse(lastUpdateString);

        // Check if the last update was more than a day ago
        if (DateTime.Now.Date > lastUpdate.Date)
        {
            totalTimeMeditatedToday = 0; 
            Debug.Log("New day detected, resetting meditation time.");
        }
        else
        {
            totalTimeMeditatedToday = PlayerPrefs.GetInt(MeditationTimeKey, 0);
        }

        if (timer.meditation_timer_set > 0)
        {
            foreach (AudioSource A in audioSource)
            {
                if (A.clip.name == timer.meditation_timer_set.ToString())
                {
                    audioSourceToPlay = A;
                    PlayAudio();
                }
            }
        }

        if (audioSourceToPlay == null)
        {
            Debug.LogError("AudioSource is not assigned.");
            return;
        }

        if (volumeSlider != null)
        {
            volumeSlider.value = audioSourceToPlay.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
        else
        {
            Debug.LogWarning("Volume slider is not assigned.");
        }
    }

    private void Update()
    {
        if (playPauseButton.action.WasPressedThisFrame())
        {
            TogglePlayPause();
        }

        // Update meditation time when audio is playing
        if (audioSourceToPlay.isPlaying && !isPaused)
        {
            meditationTimer += Time.deltaTime;

            if (meditationTimer >= 60f) // If a minute has passed
            {
                meditationTimer -= 60f; // Reset the timer for the next minute
                totalTimeMeditatedToday += 1; // Increment the total time
                PlayerPrefs.SetInt(MeditationTimeKey, totalTimeMeditatedToday); // Save the updated 
                timer.total_meditation_time = totalTimeMeditatedToday;
                happiness_slider.value += 1;
                //updated_time.Meditation_time = totalTimeMeditatedToday;
                Debug.Log("Total Time Meditated Today: " + totalTimeMeditatedToday + " minutes");
            }
        }

        // Update UI with the total meditation time (optional)
        if (TotalMeditationtime != null)
        {
            TotalMeditationtime.text = $"Total Meditation Time: {totalTimeMeditatedToday} minutes";
        }
    }

    public void PlayAudio()
    {
        if (!audioSourceToPlay.isPlaying)
        {
            audioSourceToPlay.Play();
            isPaused = false;
        }
    }

    public void StopAudio()
    {
        if (audioSourceToPlay.isPlaying)
        {
            audioSourceToPlay.Stop();
            isPaused = false;
        }
    }

    public void PauseAudio()
    {
        if (audioSourceToPlay.isPlaying)
        {
            audioSourceToPlay.Pause();
            isPaused = true;
        }
    }

    public void ResumeAudio()
    {
        if (!audioSourceToPlay.isPlaying && audioSourceToPlay.time > 0)
        {
            audioSourceToPlay.UnPause();
            isPaused = false;
        }
    }

    public void SetVolume(float volume)
    {
        audioSourceToPlay.volume = volume;
    }

    private void TogglePlayPause()
    {
        if (audioSourceToPlay.isPlaying)
        {
            PauseAudio();
        }
        else if (isPaused)
        {
            ResumeAudio();
        }
        else
        {
            PlayAudio();
        }
    }

    private void OnDestroy()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(SetVolume);
        }

        // Save the meditation time and last update date
        PlayerPrefs.SetInt(MeditationTimeKey, totalTimeMeditatedToday);
        PlayerPrefs.SetString(LastUpdateKey, DateTime.Now.ToString());
        PlayerPrefs.SetInt("PersistantMeditationTimeSet", timer.total_meditation_time);
    }
}
