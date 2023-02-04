using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
    public abstract bool IsInteractable { get; }

    public abstract void Interact(Character character);
}
