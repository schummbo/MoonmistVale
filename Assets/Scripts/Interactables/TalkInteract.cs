using UnityEngine;

public class TalkInteract : InteractableBase
{
    [SerializeField] private DialogContainer dialogContainer;

    public override bool IsInteractable => hasSomethingToSay;

    private bool hasSomethingToSay = true;
    
    public override void Interact(Character character)
    {
        GameManager.Instance.DialogSystem.Initialize(dialogContainer);
        hasSomethingToSay = false;
    }
}
