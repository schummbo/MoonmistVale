using Assets.Scripts.NotificationSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ambiance
{
    public class BuildingController : MonoBehaviour, IDayNightCycleChangeHandler
    {
        [SerializeField] private List<Building> buildings;

        public void HandleDayNightCycleChange(DayNightChangeArgs args)
        {
            if (args.State == DayNightCycleState.Evening)
            {
                foreach (var building in buildings)
                {
                    building.TurnOnPorchLight(true);
                }
            }

            if (args.State == DayNightCycleState.Morning)
            {
                foreach (var building in buildings)
                {
                    building.TurnOffPorchLight(true);
                }
            }
        }
    }
}
