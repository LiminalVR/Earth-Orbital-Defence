using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveTimer : MonoBehaviour
{
    // Start is called before the first frame update
    public Text timerLable;

    public float time;

    // Update is called once per frame
    void Update()
    {

        time -= Time.deltaTime;
        var minutes = time / 60;
        var seconds = time % 60;
        if (minutes < 0 && seconds < 0)
        {
            minutes = 0;
            seconds = 0;
            timerLable.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        }
        else
        {
            timerLable.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        }
        
        

        

        
    }

    public float getTime()
    {
        return time;
    }
}
