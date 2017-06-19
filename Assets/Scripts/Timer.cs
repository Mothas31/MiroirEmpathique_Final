using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public Text timerText;
    public float starTime;
    float t = 0;
   

    void Start()
    {
        starTime = Time.time;
      
    }

    void Update()
    {
      if(t < 4)
        {
            t = Time.time - starTime;
           
        }
      if(t > 4)
        {
            starTime += 4;
        }

        //string minutes = ((int)t / 60).ToString();
        //string seconds = (t % 60).ToString("f2");

        //timerText.text = minutes + ";" + seconds;
        

    }
       

}
