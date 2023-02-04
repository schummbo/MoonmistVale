using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject InventoryPanel;
    [SerializeField] GameObject Toolbar;
    [SerializeField] GameObject TopPanel;

    public void OnToggleInventory(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            InventoryPanel.SetActive(!InventoryPanel.activeInHierarchy);
            TopPanel.SetActive(!TopPanel.activeInHierarchy);
            Toolbar.SetActive(!Toolbar.activeInHierarchy);
        }
    }
}
