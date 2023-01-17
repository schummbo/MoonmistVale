using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragDropController : MonoBehaviour
{
    [SerializeField] private ItemSlot itemSlot;
    [SerializeField] private GameObject itemIcon;
    private RectTransform iconTransform;

    private void Awake()
    {
        itemSlot = new ItemSlot();
        iconTransform = itemIcon.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (itemIcon.activeInHierarchy)
        {
            iconTransform.position = Input.mousePosition;

            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    worldPosition.z = 0;
                    ItemSpawnManager.Instance.SpawnItem(worldPosition, itemSlot.Item, itemSlot.Count);

                    this.itemSlot.Clear();
                    this.itemIcon.SetActive(false);
                }
            }
            
        }
    }

    public void OnClick(ItemSlot inventoryItemSlot)
    {
        if (this.itemSlot.Item == null)
        {
            this.itemSlot.Copy(inventoryItemSlot);
            inventoryItemSlot.Clear();
        }
        else
        {
            var temp = new ItemSlot();
            temp.Copy(inventoryItemSlot);

            inventoryItemSlot.Copy(this.itemSlot);
            this.itemSlot = temp;
        }

        UpdateIcon();
    }

    private void UpdateIcon()
    {
        if (this.itemSlot.Item == null)
        {
            this.itemIcon.SetActive(false);
        }
        else
        {
            this.itemIcon.SetActive(true);
            this.itemIcon.GetComponent<Image>().sprite = this.itemSlot.Item.Icon;
        }
    }
}
