using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public static ItemSpawnManager Instance;

    [SerializeField] private GameObject pickUpItem; 

    void Awake()
    {
        Instance = this;
    }

    public void SpawnItem(Vector2 position, Item item, int count)
    {
        GameObject o = GameObject.Instantiate(pickUpItem, position, Quaternion.identity);
        o.SetActive(true);
        o.GetComponent<PickUpItem>().Set(item, count);
    }
}
