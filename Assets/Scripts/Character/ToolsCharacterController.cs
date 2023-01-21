using Assets.Scripts;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class ToolsCharacterController : MonoBehaviour
{
    private CharacterController characterController;
    private Rigidbody2D rigidBody2D;

    private InputAction useToolAction;

    [SerializeField] private float offsetDistance = 1f;
    [SerializeField] private float sizeOfInteractableArea = 1.2f;
    [SerializeField] private float maxSelectableDistance = 1.5f;
    [SerializeField] private MarkerManager markerManager;
    [SerializeField] private TileMapReadController tileMapReadController;
    [SerializeField] private CropsManager cropsManager;

    private Vector2 previousMousePosition;
    private float timePassed;


    private Vector3Int selectedTilePosition;
    private bool selectable;
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

        SelectTile();
        CanSelectedCheck();
        Mark();
    }

    private void SelectTile()
    {
        selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
    }

    void CanSelectedCheck()
    {
        Vector2 characterPosition = this.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        selectable = Vector2.Distance(characterPosition, mousePosition) <= maxSelectableDistance;

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

    private bool UseToolWorld() {

        var position = rigidBody2D.position + characterController.LastDirection * offsetDistance;

        var toolHittable = Utilities.GetBehaviorsNearPosition<ToolHittableBase>(position, sizeOfInteractableArea).FirstOrDefault();

        if (toolHittable != null)
        {
            toolHittable.Hit();
            return true;
        }

        return false;
    }

    public void UseToolGrid()
    {
        if (selectable)
        {
            TileBase tile = tileMapReadController.GetTileBase(selectedTilePosition);

            if (tile == null)
            {
                return;
            }

            TileData tileData = tileMapReadController.GetTileData(tile);


            if (tileData == null || !tileData.Plowable)
            {
                return;
            }

            if (cropsManager.Check(selectedTilePosition))
            {
                cropsManager.Seed(selectedTilePosition);
            }
            else
            {
                cropsManager.Plow(selectedTilePosition);
            }
            
        }
    }
}
