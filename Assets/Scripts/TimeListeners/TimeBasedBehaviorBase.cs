using UnityEngine;

public abstract class TimeBasedBehaviorBase : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.dayTimeController.OnPhaseStarted += HandlePhaseStarted;
    }

    void OnDestroy()
    {
        GameManager.Instance.dayTimeController.OnPhaseStarted -= HandlePhaseStarted;
    }

    protected abstract void HandlePhaseStarted(int phase);
}
