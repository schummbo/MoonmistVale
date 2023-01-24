using System.Collections.Generic;
using UnityEngine;

public class BuildingController : TimeBasedBehaviorBase
{
    [SerializeField] private int turnOnHour = 20;
    [SerializeField] private int turnOffHour = 5;

    [SerializeField] private List<Building> buildings;

    protected override void HandlePhaseStarted(int phase)
    {
        if (phase == TimeUtilities.GetPhaseByHour(turnOnHour))
        {
            foreach (var building in buildings)
            {
                building.TurnOnPorchLight(true);
            }
        }

        if (phase == TimeUtilities.GetPhaseByHour(turnOffHour))
        {
            foreach (var building in buildings)
            {
                building.TurnOffPorchLight(true);
            }
        }
    }
}
