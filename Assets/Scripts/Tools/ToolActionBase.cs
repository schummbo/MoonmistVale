using System.Collections.Generic;
using Assets.Scripts.ToolHittables;
using UnityEngine;

public abstract class ToolActionBase : ScriptableObject
{
    public abstract bool OnApply(Vector2 worldPoint);

}
