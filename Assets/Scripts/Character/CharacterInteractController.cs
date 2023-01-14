using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInteractController : MonoBehaviour
{
    private CharacterController characterController;
    private Rigidbody2D rigidBody2D;
    private Character character;
    [SerializeField] public HighlightController highlightController;

    [SerializeField] private float offsetDistance = 1f;
    [SerializeField] private float sizeOfInteractableArea = 1.2f;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
    }

    private void Update()
    {
        CheckForInteractables();
    }

    private void CheckForInteractables()
    {
        var position = rigidBody2D.position + characterController.LastDirection * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);
        
        foreach (var collider in colliders)
        {
            var interactable = collider.GetComponent<InteractableBase>();

            if (interactable != null && interactable.IsInteractable)
            {
                highlightController.Highlight(interactable.gameObject);
                return;
            }
        }

        highlightController.Hide();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var position = rigidBody2D.position + characterController.LastDirection * offsetDistance;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

            foreach (var collider in colliders)
            {
                var interactable = collider.GetComponent<InteractableBase>();

                if (interactable != null)
                {
                    interactable.Interact(character);
                }
            }
        }
    }
}
