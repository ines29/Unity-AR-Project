using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class setCalendarDate : MonoBehaviour
{
    private TMP_Text mytext;

    public void changeDateOfCalendar(bool isPresent)
    {
        if (isPresent)
        {
            GetComponent<TMP_Text>().text = "2023";
        }
        else
        {
            GetComponent<TMP_Text>().text = "1993";
        }
    }
}