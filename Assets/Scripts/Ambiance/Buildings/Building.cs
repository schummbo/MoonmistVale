using Assets.Scripts.Ambiance;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private RandomLight porchLight;

    public void TurnOnPorchLight(bool randomize)
    {
        porchLight.TurnOn(randomize);
    }

    public void TurnOffPorchLight(bool randomize)
    {
       porchLight.TurnOff(randomize);
    }
}
