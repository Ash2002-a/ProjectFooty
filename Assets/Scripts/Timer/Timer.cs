using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct TimerData
{
    public float seconds;
    public bool isStartTimer;
    public TimerData(float seconds, bool isStartTimer)
    {
        this.seconds = seconds;
        this.isStartTimer = isStartTimer;
    }
}
public class Timer : MonoBehaviour
{
    public TimerData timeData;
    private void Update()
    {
        if (!timeData.isStartTimer) return;

        TimerCalculator();
    }
    private void TimerCalculator()
    {
        timeData.seconds -= Time.deltaTime;
        //        Debug.Log(timeData.seconds);
        if (timeData.seconds <= 0)
        {
            StopTimer();

        }
    }
    public void StartTimer()
    {
        timeData.isStartTimer = true;
    }
    public void ResetTimer(TimerData newTimeData)
    {
        this.timeData = newTimeData;
    }
    public void StopTimer()
    {
        timeData.isStartTimer = false;

    }
    public bool IsTimerRunning()
    {
        return timeData.isStartTimer;
    }
    public TimerData GetTimerState()
    {
        // Copy values so not rewrite by getter
        TimerData timerData = this.timeData;
        return timerData;
    }
}
