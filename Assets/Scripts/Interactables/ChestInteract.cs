using UnityEngine;

public class ChestInteract : InteractableBase
{
    [SerializeField] public GameObject OpenContainer;
    [SerializeField] public GameObject ClosedContainer;
    [SerializeField] public bool IsOpen = false;

    [SerializeField] int dropCount = 5;
    [SerializeField] int amountPerDrop = 1;
    [SerializeField] float spread = 0.7f;
    [SerializeField] private Item item;

    public override bool IsInteractable => !IsOpen;

    public override void Interact(Character character)
    {
        if (!IsOpen)
        {
            IsOpen = true;
            ClosedContainer.SetActive(false);
            OpenContainer.SetActive(true);

            for (int i = 0; i < dropCount; i++)
            {
                Vector2 positionOfResourceSource = transform.position;

                positionOfResourceSource.x += spread * Random.value - spread / 2;
                positionOfResourceSource.y += spread * Random.value - spread / 2;

                ItemSpawnManager.Instance.SpawnItem(positionOfResourceSource, item, amountPerDrop);
            }

        }
    }
}
