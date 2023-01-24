using System.Collections.Generic;
using UnityEngine;

public class FireflyController : TimeBasedBehaviorBase
{
    [SerializeField] private int turnOnHour = 20;
    [SerializeField] private int turnOffHour = 5;
    [SerializeField] private List<Firefly> fireflies;

    protected override void HandlePhaseStarted(int phase)
    {
        if (phase == TimeUtilities.GetPhaseByHour(turnOnHour))
        {
            foreach (var firefly in fireflies)
            {
                firefly.TurnOn();
            }
        }

        if (phase == TimeUtilities.GetPhaseByHour(turnOffHour))
        {
            foreach (var firefly in fireflies)
            {
                firefly.TurnOff();
            }
        }
    }
}
