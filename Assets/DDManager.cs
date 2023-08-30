using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDManager : MonoBehaviour
{
    public DayData DD;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DD.reset();
    }
}
