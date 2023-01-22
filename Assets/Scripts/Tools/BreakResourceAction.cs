using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.ToolHittables;
using UnityEngine;

namespace Assets.Scripts.Tools
{
    [CreateAssetMenu(menuName = "Data/Tool Actions/Break Resource ")]
    public class BreakResourceAction : ToolActionBase
    {

        [SerializeField] float sizeOfInteractableArea = 1f;
        [SerializeField] private List<ResourceType> CanHitTypes;

        public override bool OnApply(Vector2 worldPoint)
        {
            var resourceHittable = Utilities.GetObjectsNearPosition<ResourceHittable>(worldPoint, sizeOfInteractableArea).FirstOrDefault();

            if (resourceHittable != null && resourceHittable.CanBeHit(CanHitTypes))
            {
                resourceHittable.Hit();
                return true;
            }

            return false;
        }
    }
}
