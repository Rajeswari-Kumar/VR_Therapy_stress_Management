using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carry_forward_time : MonoBehaviour
{
    public int meditation_timer_set;
    public int breathing_timer_set;
    public int total_meditation_time;
    public int total_breathing_time;
    public static Carry_forward_time instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); 
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
