using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject Toolbar;

    public void OnToggleInventory(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            Panel.SetActive(!Panel.activeInHierarchy);
            Toolbar.SetActive(!Toolbar.activeInHierarchy);
        }
    }
}
