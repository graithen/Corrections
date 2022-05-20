using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DateTimeSystem : MonoBehaviour
{
    [Header("Time Settings")]
    public TextMeshProUGUI TimeUI;
    public TextMeshProUGUI DateUI;

    public TimeFormat timeFormat = TimeFormat.Hour24;
    public DateFormat dateFormat = DateFormat.DD_MM_YYYY;

    private int startHour = 9;
    private int startMinute = 0;

    public float SecPerMinute = 1;

    [Header("Events")]
    public UnityEvent HourTrigger = new UnityEvent();
    public UnityEvent DayTrigger = new UnityEvent();
    public UnityEvent MonthTrigger = new UnityEvent();
    public UnityEvent YearTrigger = new UnityEvent();

    private string time;
    public string TimeGet { get { return time; } }
    private string date;

    private int hr;
    public int Hr { get { return hr; } set { hr = value; } }
    int min;
    public int Min { get { return hr; } set { hr = value; } }

    private int day;
    public int Day { get { return day; } set { day = value; } }
    int month;
    public int Month { get { return month; } set { month = value; } }
    int year;
    public int Year { get { return year; } set { year = value; } }

    int maxHr = 18;
    int maxMin = 60;

    int maxDay = 30;
    int maxMonth = 12;

    float timer = 0;
    public float Timer { get { return timer; } set { timer = value; } }


    bool isAM;

    private bool isPaused = false;
    public bool IsPaused { get { return isPaused; } set { isPaused = value; } }

    public enum TimeFormat
    {
        Hour24,
        Hour12
    }

    public enum DateFormat
    {
        MM_DD_YYYYY,
        DD_MM_YYYY,
        YYYY_MM_DD,
        YYYY_DD_MM
    }

    public enum DayOfWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    int DayIndex = 0;
    public DayOfWeek CurrentDay;

    public UnityEvent PerformanceReview = new UnityEvent();


    private void Awake()
    {
        //Set a date and time five years from now when no saved date and time to put
        hr = startHour;
        min = startMinute;
        day = System.DateTime.Now.Day;
        DayIndex = (int)System.DateTime.Today.DayOfWeek;
        month = System.DateTime.Now.Month;
        year = System.DateTime.Now.Year + 5;

        CalculateDaysInMonth();
        CalculateDay();

        if (hr < 12)
        {
            isAM = true;
        }
    }

    private void Update()
    {
        if (!isPaused)
            CalculateTimeDate();
    }

    void CalculateTimeDate()
    {
        if (timer >= SecPerMinute)
        {
            min++;
            if (min >= maxMin)
            {
                min = 0;
                hr++;
                HourTrigger.Invoke();
                if (hr >= maxHr)
                {
                    hr = startHour;
                    day++;
                    CalculateDay();
                    DayTrigger.Invoke();
                    if (day > maxDay)
                    {
                        day = 1;
                        month++;
                        MonthTrigger.Invoke();
                        if (month > maxMonth)
                        {
                            month = 1;
                            year++;
                            YearTrigger.Invoke();
                        }
                    }
                }
            }
            SetTimeDate();

            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    void SetTimeDate()
    {
        CalculateDaysInMonth();

        switch(timeFormat)
        {
            case TimeFormat.Hour12:
            {
                    int h;

                    if(hr >= 13)
                    {
                        h = hr - 12;
                    }
                    else if(hr == 0)
                    {
                        h = 12;
                    }
                    else
                    {
                        h = hr;
                    }

                    time = h + ":";

                    if(min <= 9)
                    {
                        time += "0" + min;
                    }
                    else
                    {
                        time += min;
                    }

                    if(isAM)
                    {
                        time += " AM";
                    }
                    else
                    {
                        time += " PM";
                    }

                break;
            }
            case TimeFormat.Hour24:
            {
                if(hr <= 9)
                {
                        time = "0" + hr + ":";                
                }
                else
                {
                        time = hr + ":";
                }
                
                if(min <= 9)
                {
                        time += "0" + min;
                }
                else
                {
                    time += min;
                }
                break;
            }
        }

        switch (dateFormat)
        {
            case DateFormat.MM_DD_YYYYY:
            {
                date = month + "/" + day + "/" + year;
                break;
            }
            case DateFormat.DD_MM_YYYY:
            {
                date = day + "/" + month + "/" + year;
                break;
            }
            case DateFormat.YYYY_MM_DD:
            {
                date = year + "/" + month + "/" + day;
                break;
            }
            case DateFormat.YYYY_DD_MM:
            {
                date = year + "/" + day + "/" + month;
                break;
            }
        }

        TimeUI.text = time;
        DateUI.text = date;
    }

    void CalculateDaysInMonth()
    {
        if(month == 2)
        {
            maxDay = 28;
        }
        if(month == 4 || month == 6 || month == 9 || month == 11)
        {
            maxDay = 30;
        }
        else
        {
            maxDay = 31;
        }
    }

    void CalculateDay()
    {
        switch (DayIndex)
        {
            case 1:
                {
                    CurrentDay = DayOfWeek.Monday;
                    break;
                }
            case 2:
                {
                    CurrentDay = DayOfWeek.Tuesday;
                    break;
                }
            case 3:
                {
                    CurrentDay = DayOfWeek.Wednesday;
                    break;
                }
            case 4:
                {
                    CurrentDay = DayOfWeek.Thursday;
                    break;
                }
            case 5:
                {
                    CurrentDay = DayOfWeek.Friday;
                    PerformanceReview.Invoke(); //invoke the performance review
                    break;
                }
            case 6:
                {
                    CurrentDay = DayOfWeek.Saturday;
                    break;
                }
            case 7:
                {
                    CurrentDay = DayOfWeek.Sunday;
                    break;
                }
        }
        
        DayIndex++;
        
        if (DayIndex > 7)
            DayIndex = 1;
    }

    //Time controls
    public void NormalSpeed()
    {
        SecPerMinute = 1;
        isPaused = false;
        Debug.Log("Time at normal speed");
    }
    
    public void FastForward()
    {
        SecPerMinute = 0.0000000000000001f;
        isPaused = false;
        Debug.Log("Fast forwarding time");
    }

    public void Pause()
    {
        isPaused = !isPaused;
        Debug.Log("Pausing time");
    }

    public string PickRandomTime()
    {
        string randomTime = "";

        int randomHour = Random.Range(startHour, maxHr);
        int randomMinute = Random.Range(startMinute, maxMin);

        switch (timeFormat)
        {
            case TimeFormat.Hour12:
            {
                int h;

                if (randomHour >= 13)
                {
                    h = randomHour - 12;
                }
                else if (hr == 0)
                {
                    h = 12;
                }
                else
                {
                    h = randomHour;
                }

                randomTime = h + ":";

                if (randomMinute <= 9)
                {
                    randomTime += "0" + randomMinute;
                }
                else
                {
                    randomTime += randomMinute;
                }

                if (isAM)
                {
                    randomTime += " AM";
                }
                else
                {
                    randomTime += " PM";
                }

                break;
            }

            case TimeFormat.Hour24:
            {
                if (randomHour <= 9)
                {
                    randomTime = "0" + randomHour + ":";
                }
                else
                {
                    randomTime = randomHour + ":";
                }

                if (randomMinute <= 9)
                {
                    randomTime += "0" + randomMinute;
                }
                else
                {
                    randomTime += randomMinute;
                }
                break;
            }
        }

        return randomTime;
    }
}
