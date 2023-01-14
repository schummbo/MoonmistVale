using UnityEngine;

public class TalkInteract : InteractableBase
{
    public override bool IsInteractable => hasSomethingToSay;

    private bool hasSomethingToSay = true;
    
    public override void Interact(Character character)
    {
        hasSomethingToSay = false;
        Debug.Log("Talking with character.");
    }
}
