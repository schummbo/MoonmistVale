using Assets.Scripts.NotificationSystem;
using System.Collections.Generic;
using UnityEngine;

public class FireflyController : MonoBehaviour, IDayNightCycleChangeHandler
{
    [SerializeField] private List<Firefly> fireflies;

    public void HandleDayNightCycleChange(DayNightChangeArgs args)
    {
        if (args.State == DayNightCycleState.Evening)
        {
            foreach (var firefly in fireflies)
            {
                firefly.TurnOn();
            }
        }

        if (args.State == DayNightCycleState.Morning)
        {
            foreach (var firefly in fireflies)
            {
                firefly.TurnOff();
            }
        }
    }
}
