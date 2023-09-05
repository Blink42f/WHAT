using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update

    public float Timeleft;
    public bool TimerOn = false;

    public TMP_Text TimerText;
    public GameObject TimerTextGrouping;

    void Start()
    {
        TimerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(TimerOn)
        {
            if(Timeleft > 0)
            {
                Timeleft -= Time.deltaTime;
                updateTimer(Timeleft);
            }
            else
            {
                Timeleft = 0;
                TimerOn = false;
                TimerTextGrouping.gameObject.SetActive(false);
               
            }
        }
  
            }
            void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }


    
}
