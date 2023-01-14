using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInteractController : MonoBehaviour
{
    private CharacterController characterController;
    private Rigidbody2D rigidBody2D;
    private PlayerInput playerInput;
    private InputAction interactAction;
    private Character character;
    [SerializeField] HighlightController highlightController;

    [SerializeField] private float offsetDistance = 1f;
    [SerializeField] private float sizeOfInteractableArea = 1.2f;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();

        interactAction = playerInput.actions["Interact"];
    }

    private void OnEnable()
    {
        interactAction.performed += Interact;
    }

    private void OnDisable()
    {
        interactAction.performed -= Interact;
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

    private void Interact(InputAction.CallbackContext context)
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
