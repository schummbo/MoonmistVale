using Assets.Scripts.PubSub;
using Cinemachine;
using UnityEngine;

public class CameraConfiner : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner confiner;
    [SerializeField] private PubSubEvents pubSubEvents;

    void Start()
    {
        pubSubEvents.OnScenePostChange += HandleScenePostChange;

        HandleScenePostChange();
    }

    private void HandleScenePostChange()
    {
        var cameraConfiner = GameObject.Find("CameraConfiner");

        if (cameraConfiner == null)
        {
            confiner.m_BoundingShape2D = null;
            return;
        }

        Collider2D bounds = cameraConfiner.GetComponent<Collider2D>();

        confiner.m_BoundingShape2D = bounds;
    }
}
