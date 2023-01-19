using Assets.Scripts.NotificationSystem;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DayTimeController : MonoBehaviour
{
    [SerializeField] int DayStartTime = 5;

    float Hours => currentTimeSeconds / 3600f;
    float Minutes => currentTimeSeconds % 3600f / 60f;

    private const float SecondsInDay = 86400;
    private float currentTimeSeconds;

    [SerializeField] float timeScale = 60f;

    [SerializeField] Color nightColor;
    [SerializeField] Color dayColor;
    [SerializeField] AnimationCurve nightTimeCurve;

    [SerializeField] Text timeText;

    [SerializeField] Light2D globalLight;

    private DayNightCycleState? previousDayNightCycleState;

    private int days = 1;


    void Awake()
    {
        currentTimeSeconds = DayStartTime * 60f * 60f;
    }

    void Update()
    {
        var valueOnDaylightCurve = nightTimeCurve.Evaluate(Hours);

        SetDaylightColor(valueOnDaylightCurve);
        SetDayAndTime();

        NotifyDayOrNight(valueOnDaylightCurve);

        currentTimeSeconds += Time.deltaTime * timeScale;

        if (currentTimeSeconds > SecondsInDay)
        {
            NextDay();
        }
    }

    private void NotifyDayOrNight(float valueOnDaylightCurve)
    {
        const float tolerance = .01f;

        DayNightCycleState? state = null;

        if (Mathf.Abs(0 - valueOnDaylightCurve) < tolerance)
        {
            state = DayNightCycleState.Day;
        }

        if (Mathf.Abs(.7f - valueOnDaylightCurve) < tolerance)
        {
            if (previousDayNightCycleState == DayNightCycleState.Night)
            {
                state = DayNightCycleState.Morning;
            }

            if (previousDayNightCycleState == DayNightCycleState.Day)
            {
                state = DayNightCycleState.Evening;
            }
        }

        if (Mathf.Abs(1 - valueOnDaylightCurve) < tolerance)
        {
            state = DayNightCycleState.Night;
        }

        if (state.HasValue && state != previousDayNightCycleState)
        {
            BroadcastMessage(IDayNightCycleChangeHandler.ChangeHandlerName, new DayNightChangeArgs(state.Value));

            previousDayNightCycleState = state;
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
}
