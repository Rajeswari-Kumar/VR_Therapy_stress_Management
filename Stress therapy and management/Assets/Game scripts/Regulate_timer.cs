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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Increment_meditation_timer()
    {
        Meditation_timer += 5;
        update_meditation_timer();
    }

    public void Decrement_meditation_timer()
    {
        if(Meditation_timer > 0)
        {
            Meditation_timer -= 5;
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
        update_Breathe_timer();
    }

    public void Decrement_Breathe_timer()
    {
        if (Breathe_timer > 0)
        {
            Breathe_timer -= 5;
            update_Breathe_timer();
        }
    }

    public void update_Breathe_timer()
    {
        Breathe_minutes.text = Breathe_timer.ToString();
    }
}
