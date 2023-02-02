using Assets.Scripts.PubSub;
using UnityEngine;

public abstract class TimeBasedBehaviorBase : MonoBehaviour
{
    [SerializeField] protected PubSubEvents pubSubEvents;

    protected void OnEnable()
    {
        pubSubEvents.OnPhaseStarting += HandlePhaseStarted;
    }

    protected void OnDisable()
    {
        pubSubEvents.OnPhaseStarting -= HandlePhaseStarted;
    }

    protected abstract void HandlePhaseStarted(int phase);
}
