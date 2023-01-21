using Assets.Scripts;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToolsCharacterController : MonoBehaviour
{
    private CharacterController characterController;
    private Rigidbody2D rigidBody2D;

    private InputAction useToolAction;

    [SerializeField] private float offsetDistance = 1f;
    [SerializeField] private float sizeOfInteractableArea = 1.2f;
    [SerializeField] private MarkerManager markerManager;
    [SerializeField] private TileMapReadController tileMapReadController;

    private Vector2 previousMousePosition;
    private float lastMouseMove;
    private float timePassed;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timePassed += Time.deltaTime;

        if (previousMousePosition.Equals(Input.mousePosition))
        {
            if (timePassed > 3)
            {
                markerManager.IsMarking = false;
            }
        }
        else
        {
            markerManager.IsMarking = true;
            previousMousePosition = Input.mousePosition;
            timePassed = 0;
        }

        Mark();
    }

    private void Mark()
    {
        var gridPosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
        markerManager.markedCell = gridPosition;
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
    public void OnUseTool(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var position = rigidBody2D.position + characterController.LastDirection * offsetDistance;

            var toolHittables = Utilities.GetBehaviorsNearPosition<ToolHittableBase>(position, sizeOfInteractableArea);

            toolHittables.FirstOrDefault()?.Hit();
        }
    }
}
