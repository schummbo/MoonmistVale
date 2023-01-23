using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToolsCharacterController : MonoBehaviour
{
    private CharacterController characterController;
    private Rigidbody2D rigidBody2D;

    [SerializeField] private float offsetDistance = 1f;
    [SerializeField] private int selectableCellRadius = 1;
    [SerializeField] private MarkerManager markerManager;
    [SerializeField] private TileMapReadController tileMapReadController;
    [SerializeField] private ToolbarController toolbarController;
    private Animator animator;

    private Vector2 previousMousePosition;
    private float timePassed;

    private Vector3Int selectedTilePosition;
    private bool selectable;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

        SelectTile();
        CanSelectCheck();
        Mark();
    }

    private void SelectTile()
    {
        selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
    }

    void CanSelectCheck()
    {
        Vector2 characterPosition = this.transform.position;
        var nearbyCells = tileMapReadController.GetCellsAroundPosition(characterPosition, selectableCellRadius);

        var mouseCell = tileMapReadController.GetGridPosition(Input.mousePosition, true);
        selectable = nearbyCells.Any(cell => cell.Equals(mouseCell));

        if (markerManager.IsMarking)
        {
            markerManager.IsMarking = selectable;
        }
    }

    private void Mark()
    {
        markerManager.markedCell = selectedTilePosition;
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
            if (!UseToolWorld())
            {
                UseToolGrid();
            }
        }
    }

    private bool UseToolWorld() 
    {
        var position = rigidBody2D.position + characterController.MotionVector * offsetDistance;

        Item selectedTool = toolbarController.GetSelectedTool();

        if (selectedTool != null && selectedTool.onAction != null)
        {
            animator.SetTrigger("PerformAction");
            if (selectedTool.onAction.OnApply(position))
            {
                if (selectedTool.onItemUsed != null)
                {
                    selectedTool.onItemUsed.OnItemUsed(selectedTool, GameManager.Instance.InventoryContainer);
                    return true;
                }
            }
        }

        return false;
    }

    public bool UseToolGrid()
    {
        if (selectable)
        {
            Item selectedTool = toolbarController.GetSelectedTool();

            if (selectedTool != null && selectedTool.onTilemapAction != null)
            {
                animator.SetTrigger("PerformAction");
                if (selectedTool.onTilemapAction.OnApplyToTileMap(selectedTilePosition, tileMapReadController))
                {
                    if (selectedTool.onItemUsed != null)
                    {
                        selectedTool.onItemUsed.OnItemUsed(selectedTool, GameManager.Instance.InventoryContainer);
                        return true;
                    }
                }
            }
        }
        
        return false;
    }
}
