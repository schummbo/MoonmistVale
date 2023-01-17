using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Player;

    public ItemContainer InventoryContainer;
    public ItemDragDropController ItemDragDropController;

    private void Awake()
    {
        Instance = this;
    }
}
