using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Player;

    public ItemContainer InventoryContainer;
    public ItemDragDropController ItemDragDropController;
    public DayTimeController dayTimeController;
    public DialogSystem DialogSystem;

    private void Awake()
    {
        Instance = this;
    }
}
