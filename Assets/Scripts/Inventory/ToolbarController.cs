using System;
using UnityEngine;

public class ToolbarController : MonoBehaviour
{
    [SerializeField] private int toolbarSize = 9;
    private int selectedTool = 0;

    public Action<int> OnChange;

    void Update()
    {
        float delta = Input.mouseScrollDelta.y;

        if (delta != 0)
        {
            if (delta > 0 && selectedTool < toolbarSize - 1)
            {
                selectedTool += 1;
            }
            else if (delta < 0 && selectedTool > 0)
            {
                selectedTool -= 1;
            }
            
            OnChange?.Invoke(selectedTool);
        }
    }

    public void SetSelectedTool(int id)
    {
        selectedTool = id;
    }
}
