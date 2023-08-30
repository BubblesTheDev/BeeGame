using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewDayData", menuName = "Day Data")]
public class DayData : ScriptableObject
{

    public int DayNum;

    public void reset()
    {
        DayNum = 0;
    }

}
