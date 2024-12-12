using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Regulate_timer : MonoBehaviour
{
    public static Regulate_timer Instance;
    public TMP_Text Meditation_minutes;
    public int Meditation_timer;
    public TMP_Text Breathe_minutes;
    public int Breathe_timer;
    public TMP_Text Total_time_meditation;
    public TMP_Text Total_time_breathing;
    Carry_forward_time time_to_carry_forward;
    // Start is called before the first frame update
    void Start()
    {
        time_to_carry_forward = FindObjectOfType<Carry_forward_time>();
        Total_time_meditation.text = time_to_carry_forward.total_meditation_time.ToString();
        Total_time_breathing.text = time_to_carry_forward.total_breathing_time.ToString();
        time_to_carry_forward.meditation_timer_set = 0;
        time_to_carry_forward.breathing_timer_set = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void Increment_meditation_timer()
    {
        Meditation_timer += 5;
        time_to_carry_forward.meditation_timer_set += 5;
        update_meditation_timer();
    }

    public void Decrement_meditation_timer()
    {
        if(Meditation_timer > 0)
        {
            Meditation_timer -= 5;
            time_to_carry_forward.meditation_timer_set -= 5;
            update_meditation_timer();
        }
    }

    public void update_meditation_timer()
    {
        Meditation_minutes.text = Meditation_timer.ToString();
    }

    public void Increment_breathe_timer()
    {
        Breathe_timer += 5;
        time_to_carry_forward.breathing_timer_set += 5;
        update_Breathe_timer();
    }

    public void Decrement_Breathe_timer()
    {
        if (Breathe_timer > 0)
        {
            Breathe_timer -= 5;
            time_to_carry_forward.breathing_timer_set -= 5;
            update_Breathe_timer();
        }
    }

    public void update_Breathe_timer()
    {
        Breathe_minutes.text = Breathe_timer.ToString();
    }
}
