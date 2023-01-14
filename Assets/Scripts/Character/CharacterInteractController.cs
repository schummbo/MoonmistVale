using System.Linq;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInteractController : MonoBehaviour
{
    private CharacterController characterController;
    private Rigidbody2D rigidBody2D;
    private Character character;
    private const float OffsetDistance = 1f;
    private const float SizeOfInteractableArea = .5f;

    [SerializeField] public HighlightController highlightController;
    
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
    }

    private void Update()
    {
        HighlightNearbyInteractables();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var interactable = GetNearbyInteractable();
            interactable?.Interact(character);
        }
    }

    private void HighlightNearbyInteractables()
    {
        var firstInteractable = GetNearbyInteractable();

        if (firstInteractable != null)
        {
            highlightController.Highlight(firstInteractable.gameObject);
        }
        else
        {
            highlightController.Hide();
        }
    }

    private InteractableBase GetNearbyInteractable()
    {
        var position = rigidBody2D.position + characterController.LastDirection * OffsetDistance;

        var interactables = Utilities.GetBehaviorsNearPosition<InteractableBase>(position, SizeOfInteractableArea);

        var firstInteractable = interactables.FirstOrDefault(i => i.IsInteractable);
        return firstInteractable;
    }
}
