using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraConfiner : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner confiner;

    void Start()
    {
        UpdateBounds();
    }

    public void UpdateBounds()
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
