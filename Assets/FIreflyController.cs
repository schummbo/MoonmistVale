using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.NotificationSystem;
using UnityEngine;

public class FIreflyController : MonoBehaviour, IDayNightCycleChangeHandler
{
    [SerializeField] private List<FireflyMovement> fireflies;


    public void HandleDayNightCycleChange(DayNightChangeArgs args)
    {
        if (args.State == DayNightCycleState.Night)
        {
            foreach (var fireflyMovement in fireflies)
            {
                fireflyMovement.gameObject.SetActive(true);
            }
        }

        if (args.State == DayNightCycleState.Day)
        {
            foreach (var fireflyMovement in fireflies)
            {
                fireflyMovement.gameObject.SetActive(false);
            }
        }
    }
}
