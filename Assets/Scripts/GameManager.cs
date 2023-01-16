using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Player;

    [SerializeField] public ItemContainer InventoryContainer;

    private void Awake()
    {
        Instance = this;
    }
}
