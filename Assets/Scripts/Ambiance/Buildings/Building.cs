using Assets.Scripts.Ambiance;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private RandomLight porchLight;

    public bool IsToggling => porchLight.IsToggling;

    void Start()
    {
        BuildingController.Instance.AddBuilding(this);
    }

    public void TurnOnPorchLight(bool randomize)
    {
        if (!porchLight.IsOn)
            porchLight.TurnOn(randomize);
    }

    public void TurnOffPorchLight(bool randomize)
    {
        if (porchLight.IsOn)
            porchLight.TurnOff(randomize);
    }
}
