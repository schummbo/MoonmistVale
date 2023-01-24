using UnityEngine;

public class ChestInteract : InteractableBase
{
    [SerializeField] public GameObject OpenContainer;
    [SerializeField] public GameObject ClosedContainer;
    [SerializeField] public bool IsOpen = false;
    
    public override bool IsInteractable => !IsOpen;

    public override void Interact(Character character)
    {
        if (!IsOpen)
        {
            IsOpen = true;
            ClosedContainer.SetActive(false);
            OpenContainer.SetActive(true);
        }
    }
}
