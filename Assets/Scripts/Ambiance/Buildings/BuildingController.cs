using Assets.Scripts.PubSub;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : TimeBasedBehaviorBase
{
    [SerializeField] private int turnOnHour = 20;
    [SerializeField] private int turnOffHour = 5;

    private List<Building> buildings;

    public static BuildingController Instance;

    private bool randomizeLights = true;

    void Awake()
    {
        Instance = this;
        buildings = new List<Building>();
    }

    new void OnEnable()
    {
        pubSubEvents.OnScenePreChange += HandleScenePreChange;
        pubSubEvents.OnScenePostChange += HandleScenePostChange;
        base.OnEnable();
    }

    new void OnDisable()
    {
        pubSubEvents.OnScenePreChange -= HandleScenePreChange;
        base.OnDisable();
    }

    private void HandleScenePostChange()
    {
        randomizeLights = false;
        int phase = GameManager.Instance.dayTimeController.GetCurrentPhase();
        HandlePhaseStarted(phase);
    }

    private void HandleScenePreChange()
    {
        buildings.Clear();
    }

    public void AddBuilding(Building building)
    {
        buildings.Add(building);
    }

    protected override void HandlePhaseStarted(int phase)
    {
        if (phase >= TimeUtilities.GetPhaseByHour(turnOnHour) || phase <= TimeUtilities.GetPhaseByHour(turnOffHour))
        {
            foreach (var building in buildings)
            {
                building.TurnOnPorchLight(randomizeLights);
            }
        }
        else
        {
            foreach (var building in buildings)
            {
                building.TurnOffPorchLight(randomizeLights);
            }
        }

        randomizeLights = true;
    }
}
