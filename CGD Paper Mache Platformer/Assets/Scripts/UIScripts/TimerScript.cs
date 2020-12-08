using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public TextMeshProUGUI timer_text;
    public float time;
    float msec;
    float sec;
    float min;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        while (true)
        {
            time += Time.deltaTime;
            msec = (int)((time - (int)time) * 100);
            sec = (int)(time % 60);
            min = (int)(time / 60 % 60);

            timer_text.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);

            yield return null;
        }
    }
}


