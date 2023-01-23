using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToolbarController : MonoBehaviour
{
    [SerializeField] private int toolbarSize = 9;
    private int selectedTool = 0;

    public Action<int> OnChange;

    public void OnScroll(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            var delta = obj.ReadValue<float>();

            if (delta != 0)
            {
                var originalSelectedTool = selectedTool;

                if (delta > 0)
                {
                    do
                    {
                        selectedTool++;
                        if (selectedTool == toolbarSize)
                        {
                            selectedTool = 0;
                        }

                        if (GetSelectedTool() != null)
                        {
                            break;
                        }
                    } while (selectedTool != originalSelectedTool);
                }
                else
                {
                    do
                    {
                        selectedTool--;
                        if (selectedTool == -1)
                        {
                            selectedTool = toolbarSize - 1;
                        }

                        if (GetSelectedTool() != null)
                        {
                            break;
                        }
                    } while (selectedTool != originalSelectedTool);
                }

                OnChange?.Invoke(selectedTool);
            }
        }
    }

    public void SetSelectedTool(int id)
    {
        selectedTool = id;
    }

    public Item GetSelectedTool()
    {
        return GameManager.Instance.InventoryContainer.ItemSlots[selectedTool].Item;
    }
    
    public void OnQuickToolSelection(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (int.TryParse(context.control.name, out int numPressed))
            {
                numPressed -= 1;

                if (GameManager.Instance.InventoryContainer.ItemSlots[numPressed].Item != null)
                {
                    SetSelectedTool(numPressed);
                    OnChange?.Invoke(selectedTool);
                }
            }
        }
    }
}
