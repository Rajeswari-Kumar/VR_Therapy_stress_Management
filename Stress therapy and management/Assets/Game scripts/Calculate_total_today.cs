using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calculate_total_today : MonoBehaviour
{
    public int total_time_spent_meditation = 0;
    public int total_time_spent_breathing = 0;
    public const string total_meditation_time = "total_time_spent_meditation";
    public TMP_Text total_time_meditate_text;
    public const string meditate_time_text = "total_time_meditate_text";

    public const string total_breathing_time = "total_time_spent_breathing";
    public TMP_Text total_time_breathing_text;
    public const string breathe_time_text = "total_time_breathing_text";

    void Start()
    {
        total_time_meditate_text.text = total_time_spent_meditation.ToString();
        total_time_breathing_text.text = total_time_spent_breathing.ToString();
    }

    void Update()
    {
        update_overall_time();
    }


    public void update_overall_time()
    {
        total_time_meditate_text.text = total_time_spent_meditation.ToString();
        total_time_breathing_text.text = total_time_spent_breathing.ToString();
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt(total_meditation_time, total_time_spent_meditation);
        PlayerPrefs.SetString(meditate_time_text, total_time_spent_meditation.ToString());


        PlayerPrefs.SetInt(total_breathing_time, total_time_spent_breathing);
        PlayerPrefs.SetString(breathe_time_text, total_time_spent_breathing.ToString());
    }
}
