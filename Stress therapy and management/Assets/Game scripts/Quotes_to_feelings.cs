using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Quotes_to_feelings : MonoBehaviour
{
    public GameObject feel_canvas;
    public GameObject quote_gameObject;
    public TMP_Text quote;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hide_canvas()
    {
        feel_canvas.SetActive(false);
        quote_gameObject.SetActive(true);
    }
    public void sad_quote()
    {
        Hide_canvas();
        quote.text = "Stars can’t shine without darkness. Even in your toughest moments, remember that this struggle is shaping your strength and resilience. You are capable of overcoming anything." ;
    }

    public void BitterSweet()
    {
        Hide_canvas();
        quote.text = "Every ending has a beginning, and every goodbye brings the hope of a new hello.";
    }
    public void Happy()
    {
        Hide_canvas();
        quote.text = "'Dwell on the beauty of life. Watch the stars, and see yourself running with them.' – Marcus Aurelius";
    }
}
