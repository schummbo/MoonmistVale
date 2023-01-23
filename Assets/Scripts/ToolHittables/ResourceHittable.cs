using System.Collections.Generic;
using Assets.Scripts.ToolHittables;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ResourceHittable : ToolHittableBase
{ 
    [SerializeField] int dropCount = 5;
    [SerializeField] int amountPerDrop = 1;
    [SerializeField] float spread = 0.7f;
    [SerializeField] private Item item;
    [SerializeField] private ResourceType resourceType;


    /// <summary>
    /// When a tree is hit, it will split into <see cref="dropCount"/> logs.
    /// </summary>
    public override void Hit()
    {
        for (int i = 0; i < dropCount; i++)
        {
            Vector2 positionOfResourceSource = transform.position;

            positionOfResourceSource.x += spread * Random.value - spread / 2;
            positionOfResourceSource.y += spread * Random.value - spread / 2;

            ItemSpawnManager.Instance.SpawnItem(positionOfResourceSource, item, amountPerDrop);
        }
        
        Destroy(gameObject);
    }

    public override bool CanBeHit(List<ResourceType> hittableTypes)
    {
        return hittableTypes.Contains(this.resourceType);
    }
}
