using System;
using Assets.Scripts.PubSub;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DayTimeController : MonoBehaviour
{
    [SerializeField] int dayStartTime = 5;

    float Hours => currentTimeSeconds / 3600f;

    float Minutes => currentTimeSeconds % 3600f / 60f;

    private float currentTimeSeconds;

    [SerializeField] float timeScale = 60f;

    [SerializeField] Color nightColor;
    [SerializeField] Color dayColor;
    [SerializeField] AnimationCurve nightTimeCurve;

    [SerializeField] Text timeText;

    [SerializeField] Light2D globalLight;

    [SerializeField] private PubSubEvents pubSubEvents;

    private int days = 1;

    void Awake()
    {
        currentTimeSeconds = dayStartTime * 60f * 60f;
    }

    void Update()
    {
        currentTimeSeconds += Time.deltaTime * timeScale;

        float valueOnDaylightCurve = nightTimeCurve.Evaluate(Hours);

        SetDaylightColor(valueOnDaylightCurve);

        SetDayAndTime();

        if (currentTimeSeconds > TimeUtilities.SecondsInDay)
        {
            NextDay();
        }

        CallTimeAgents();
    }

    private int previousPhase = 0;

    private void CallTimeAgents()
    {
        int currentPhase = TimeUtilities.GetPhase(currentTimeSeconds);

        if (previousPhase != currentPhase)
        {
            previousPhase = currentPhase;
            
            pubSubEvents.OnPhaseStarting?.Invoke(currentPhase);
        }
    }
    
    private void SetDaylightColor(float valueOnDaylightCurve)
    {
        globalLight.color = GetDaylightColor(valueOnDaylightCurve);
    }

    private void SetDayAndTime()
    {
        timeText.text = $"Day {days} {Mathf.Floor(Hours):00}:{Mathf.Floor(Minutes):00}";
    }

    private Color GetDaylightColor(float value)
    {
        return Color.Lerp(this.dayColor, nightColor, value);
    }

    private void NextDay()
    {
        currentTimeSeconds = 0;
        days += 1;
    }

    public int GetCurrentPhase()
    {
        return TimeUtilities.GetPhase(currentTimeSeconds);
    }
}
