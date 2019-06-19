using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class WaveTimer : MonoBehaviour
{
    // Start is called before the first frame update
    public Text timerLable;

    public float time;
    public Button button;
    float startT;
    public bool start = true;
    public AudioSource audi1;
    public AudioSource audi2;

    // Update is called once per frame

    private void Start()
    {
        button.onClick.AddListener(StartG);
        startT = time;
    }
    void Update()
    {
        audi1.Pause();
        audi2.Pause();
        if (start == true)
        {
            
            
            time -= Time.deltaTime;
            string minutes = Mathf.Floor(time / 60).ToString();
            string seconds = ((int)(time % 60)).ToString();
            int i = 0;
            int u = 0;
            i = System.Int32.Parse(seconds);
            u = System.Int32.Parse(minutes);
            string str = "0";
            if(i < 10 )
            {
                timerLable.text = string.Format("{0} : {1:00}", minutes, str + seconds);
            }
            else
            {
                
                
                timerLable.text = string.Format("{0} : {1:00}", minutes, seconds);
            }

            if (!audi2.isPlaying)
            {

                audi1.UnPause();
            }
            if (i <= 60 && u < 1)
            {
                audi2.UnPause();
                audi1.Pause();
            }
        }







    }

    public float getTime()
    {
        return time;
    }

    public float GetStart()
    {
        return startT;
    }
    void StartG()
    {
        if (start == false)
        {
            start = true;
            button.gameObject.SetActive(false);
            return;
        }
        if (start == true)
        {
            start = false;
            return;
        }
    }
}
