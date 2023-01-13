using UnityEngine;
using UnityEngine.InputSystem;

public class ToolsCharacterController : MonoBehaviour
{
    private CharacterController characterController;
    private Rigidbody2D rigidBody2D;
    private PlayerInput playerInput;

    private InputAction useToolAction;

    [SerializeField] private float offsetDistance = 1f;
    [SerializeField] private float sizeOfInteractableArea = 1.2f;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        useToolAction = playerInput.actions["UseTool"];
    }

    void OnEnable()
    {
        useToolAction.performed += UseTool;
    }

    void OnDisable()
    {
        useToolAction.performed -= UseTool;
    }
    
    /// <summary>
    /// Checks the direction the character is facing.
    /// If any colliders in the area in front of the character has
    /// a ToolHittableBase component, perform the hit.
    ///
    /// Whatever the item was will determine what happens when it is hit.
    ///
    /// For example, a tree will split into x number of logs.
    /// </summary>
    private void UseTool(InputAction.CallbackContext context)
    {
        var position = rigidBody2D.position + characterController.LastDirection * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (var collider in colliders)
        {
            var hit = collider.GetComponent<ToolHittableBase>();

            if (hit != null)
            {
                hit.Hit();
            }
        }
    }
}
