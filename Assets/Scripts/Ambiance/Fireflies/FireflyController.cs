using System.Collections.Generic;
using UnityEngine;

public class FireflyController : TimeBasedBehaviorBase
{
    [SerializeField] private int turnOnHour = 20;
    [SerializeField] private int turnOffHour = 5;
    private List<Firefly> fireflies;

    public static FireflyController Instance;

    void Awake()
    {
        Instance = this;
        fireflies = new List<Firefly>();
    }

    public void AddFirefly(Firefly firefly)
    {
        fireflies.Add(firefly);
    }

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
