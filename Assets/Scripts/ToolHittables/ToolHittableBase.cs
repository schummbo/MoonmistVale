using System.Collections.Generic;
using Assets.Scripts.ToolHittables;
using UnityEngine;

public abstract class ToolHittableBase : MonoBehaviour
{
    public abstract void Hit();


    public abstract bool CanBeHit(List<ResourceType> resourceTypes);
}
