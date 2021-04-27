using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameTime : MonoBehaviour
{
    public float timeStart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeStart += Time.deltaTime;
    }

    void OnDisable()
    {
        string Time = TimeFormatter(timeStart);
        PlayerPrefs.SetString("time", Time);
    }

    //time formatter (credit to form post found here https://forum.unity.com/threads/convert-float-to-time-minutes-and-seconds.676414/)
    public static string TimeFormatter( float seconds, bool forceHHMMSS = false)
    {
        float secondsRemainder = Mathf.Floor( (seconds % 60) * 100) / 100.0f;
        int minutes = ((int)(seconds / 60)) % 60;
        int hours = (int)(seconds / 3600);
 
        if (!forceHHMMSS)
        {
            if (hours == 0)
            {
                return System.String.Format ("{0:00}:{1:00.00}", minutes, secondsRemainder);
            }
        }
        return System.String.Format ("{0}:{1:00}:{2:00}", hours, minutes, secondsRemainder);
    }
}
