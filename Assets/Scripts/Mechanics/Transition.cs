using Cinemachine;
using System;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] TransitionType transitionType;
    [SerializeField] private string sceneNameToTransition;
    [SerializeField] private Vector3 targetPosition;
    private Transform destination;

    void Start()
    {
        destination = transform.GetChild(1);
    }

    public void InitiateTransition(Transform toTransition)
    {
        switch (transitionType)
        {
            case TransitionType.Warp:
                CinemachineBrain currentCamera = Camera.main.GetComponent<CinemachineBrain>();
                toTransition.position = new Vector3(destination.position.x, destination.position.y, toTransition.position.z);
                currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(toTransition, destination.position - toTransition.position);

                break;
            case TransitionType.Scene:
                GameSceneManager.Instance.InitSceneTransition(sceneNameToTransition, targetPosition);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
