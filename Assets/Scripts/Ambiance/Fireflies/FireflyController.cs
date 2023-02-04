using System.Collections.Generic;
using UnityEngine;

public class FireflyController : TimeBasedBehaviorBase
{
    [SerializeField] private int turnOnHour = 20;
    [SerializeField] private int turnOffHour = 5;
    private List<Firefly> fireflies;

    public static FireflyController Instance;

    private bool randomizeLights = true;

    void Awake()
    {
        Instance = this;
        fireflies = new List<Firefly>();
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
        pubSubEvents.OnScenePostChange -= HandleScenePostChange;
        base.OnDisable();
    }

    private void HandleScenePostChange()
    {
        int phase = GameManager.Instance.dayTimeController.GetCurrentPhase();
        randomizeLights = false;
        HandlePhaseStarted(phase);
    }

    private void HandleScenePreChange()
    {   
        fireflies.Clear();
    }

    public void AddFirefly(Firefly firefly)
    {
        fireflies.Add(firefly);
    }

    protected override void HandlePhaseStarted(int phase)
    {
        if (phase >= TimeUtilities.GetPhaseByHour(turnOnHour) || phase <= TimeUtilities.GetPhaseByHour(turnOffHour))
        {
            foreach (var firefly in fireflies)
            {
                firefly.TurnOn(randomizeLights);
            }
        } 
        else
        {
            foreach (var firefly in fireflies)
            {
                firefly.TurnOff(randomizeLights);
            }
        }

        randomizeLights = true;
    }
}
