using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject Panel;

    public void OnToggleInventory(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            Panel.SetActive(!Panel.activeInHierarchy);
        }
    }
}
