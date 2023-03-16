using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    public bool inPresentTime = true;
    public setCalendarDate calendarDate;
    //public GameObject vuforiaStuff;
    public GameObject targets;

    // Start is called before the first frame update
    void Start()
    {
        //calendarDate.changeDateOfCalendar(inPresentTime);
        //vuforiaStuff.SetActive(false);
    }

    public void setTime(int year)
    {
        if (year == 2023)
        {
            inPresentTime = true;
            calendarDate.changeDateOfCalendar(inPresentTime);
            targets.SetActive(true);
            //Prüfe wassersensor
            //vuforiaStuff.SetActive(true);
            //unser Stuff
        } else
        {
            inPresentTime = false;
            calendarDate.changeDateOfCalendar(inPresentTime);
            //vuforiaStuff.SetActive(false); //unser Stuff
            targets.SetActive(false);
        }
    }
}
