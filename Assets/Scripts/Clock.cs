using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Clock : MonoBehaviour
{
    float secondsPerInGameDay = 6f;    //normally it's 86400f;
    
    float day;
    float analogModifier = 2f;

    [SerializeField] Image background;

    [SerializeField] Transform hourClockHand;
    [SerializeField] Transform minuteClockHand;
    [SerializeField] Transform secondsClockHand;

    [SerializeField] TextMeshProUGUI clockText;
    [SerializeField] TextMeshProUGUI daysText;

    #region VALUES
    float currentHour;

    string hoursString;
    string minutesString;
    string secondsString;
    #endregion

    int daysToFinish = 30;
    int daysPassed = 1;
        
    void Update()
    {
        RotateClock();
    }
    private void FixedUpdate()
    {
        // days counting put into fixed update due to issues caused by update specifics
        float dayNormalized = day % 1f;
        float hoursPerDay = 24f;
        
        float hours = dayNormalized * hoursPerDay;
        if (hours >= 23.99)
        {
            daysPassed++;
        }
        daysText.text = daysPassed.ToString();

        //here changes colour of background
        if (currentHour >= 11 && currentHour < 20)
        {
            background.color = Color.green;
        }
        else if (currentHour >= 20 || currentHour < 6)
        {
            background.color = Color.blue;
        }
        else if (currentHour <= 11 && currentHour >= 6)
        {
            background.color = Color.white;
        }
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


        //these is values of 24h clock
        currentHour = Mathf.Floor(dayNormalized * hoursPerDay);

        hoursString = currentHour.ToString("00");
        minutesString = Mathf.Floor((dayNormalized * hoursPerDay) % 1f * minutesPerHour).ToString("00");
        secondsString = Mathf.Floor(((dayNormalized * hoursPerDay) * minutesPerHour) % 1f * minutesPerHour).ToString("00");       
        

        //visualization of 24h clock
        clockText.text = hoursString + ":" + minutesString + ":" + secondsString;


    }

    public void PeriodOfDay()
    {
        if (currentHour >= 11 && currentHour < 20)
        {
            Debug.Log("Day");
        }
        else if(currentHour >= 20 || currentHour < 6)
        {
            Debug.Log("Night");
        }
        else if( currentHour <= 11 && currentHour >= 6)
        {
            Debug.Log("Morning");
        }
    }

    public void CurrentTime()
    {
        Debug.Log(hoursString + ":" + minutesString + ":" + secondsString);
    }

    public void TimeSinceStart()
    {
        Debug.Log("Day: " + daysPassed + "  Current time is " + hoursString + ":" + minutesString + ":" + secondsString);
    }
}
