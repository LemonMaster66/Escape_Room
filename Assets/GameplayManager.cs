using System;
using UnityEngine;
using VInspector;

public class GameplayManager : MonoBehaviour
{
    public bool RocketActive;
    [Space(10)]
    [ReadOnly] public bool Running;
    [Range(0,300)] public float rocketTimer = 300;
    public string rocketTimerDisplay;
    //[Space(8)]


    void Awake()
    {
        
    }

    void Update()
    {
        if(RocketActive) RocketTimerLogic();
    }


    void RocketTimerLogic()
    {
        rocketTimer = Math.Clamp(rocketTimer - Time.deltaTime, 0, 300);
        if(Running && rocketTimer == 0)
        {
            Running = false;
            Debug.Log("Done");
        }
        if(rocketTimer != 0) Running = true;

        // Clock Display Logic
        int timerSeconds = (int)Math.Ceiling(rocketTimer);
        int timerMinutes = timerSeconds / 60;
        int remainingSeconds = timerSeconds % 60;

        rocketTimerDisplay = timerMinutes + ":" + remainingSeconds.ToString("D2");
    }
}
