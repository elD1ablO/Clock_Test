using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Clock : MonoBehaviour
{
    float secondsPerInGameDay = 6f;    //it's 86400f;
    
    float day;
    float analogModifier = 2f;

    [SerializeField] Transform hourClockHand;
    [SerializeField] Transform minuteClockHand;
    [SerializeField] Transform secondsClockHand;

    [SerializeField] TextMeshProUGUI clockText;
    [SerializeField] TextMeshProUGUI daysText;

    int daysToFinish = 30;

    int daysPassed = 1;
        
    void Update()
    {
        RotateClock();
    }

    void RotateClock()
    {
        day += Time.deltaTime / secondsPerInGameDay;

        float dayNormalized = day % 1f;

        //hour clockhand rotation
        float rotationPerDay = 360f;
        hourClockHand.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationPerDay * analogModifier);

        //minutes clockhand rotation
        float hoursPerDay = 24f;
        minuteClockHand.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationPerDay * hoursPerDay);

        //seconds clockhand rotation
        float minutesPerHour = 60f;
        secondsClockHand.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationPerDay * hoursPerDay * minutesPerHour);

        float hours = dayNormalized * hoursPerDay;

        string hoursString = Mathf.Floor(dayNormalized * hoursPerDay).ToString("00");
        string minutesString = Mathf.Floor((dayNormalized * hoursPerDay) % 1f * minutesPerHour).ToString("00");
        string secondsString = Mathf.Floor(((dayNormalized * hoursPerDay) * minutesPerHour) % 1f * minutesPerHour).ToString("00");
        
        clockText.text = hoursString + ":" + minutesString + ":" + secondsString;


        if(hours >= 23.999)
        {
            daysPassed++;
        }
        daysText.text = daysPassed.ToString();
        
    }
}
