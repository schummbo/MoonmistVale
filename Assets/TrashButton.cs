using UnityEngine;
using UnityEngine.EventSystems;

public class TrashButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.ItemDragDropController.Trash();
    }
}
