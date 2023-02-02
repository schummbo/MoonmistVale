using System.Collections.Generic;
using UnityEngine;

public class BuildingController : TimeBasedBehaviorBase
{
    [SerializeField] private int turnOnHour = 20;
    [SerializeField] private int turnOffHour = 5;

    private List<Building> buildings;

    public static BuildingController Instance;

    void Awake()
    {
        Instance = this;
        buildings = new List<Building>();
    }

    public void AddBuilding(Building building)
    {
        buildings.Add(building);
    }

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
