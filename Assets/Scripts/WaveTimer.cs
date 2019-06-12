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
    private bool start = false;
    // Update is called once per frame

    private void Start()
    {
        button.onClick.AddListener(StartG);
    }
    void Update()
    {
        if(start == true)
        {
             time -= Time.deltaTime;
        string minutes = Mathf.Floor(time / 60).ToString();
        string seconds = ((int)(time % 60)).ToString();

        if (float.Parse(minutes, CultureInfo.InvariantCulture.NumberFormat) <=0 && float.Parse(seconds,CultureInfo.InvariantCulture.NumberFormat) <=0)
        {

            timerLable.text = string.Format("{0}:{1}", 0, 0);
        }
        else
        {
            timerLable.text = string.Format("{0} : {1:00}", minutes, seconds);
        }
        }
       
        
        

        

        
    }

    public float getTime()
    {
        return time;
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
