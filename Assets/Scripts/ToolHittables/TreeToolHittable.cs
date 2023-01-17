using UnityEngine;

public class TreeToolHittable : ToolHittableBase
{ 
    [SerializeField] int dropCount = 5;
    [SerializeField] int amountPerDrop = 1;
    [SerializeField] float spread = 0.7f;
    [SerializeField] private Item item;


    /// <summary>
    /// When a tree is hit, it will split into <see cref="dropCount"/> logs.
    /// </summary>
    public override void Hit()
    {
        for (int i = 0; i < dropCount; i++)
        {
            Vector2 positionOfLog = transform.position;

            positionOfLog.x += spread * Random.value - spread / 2;
            positionOfLog.y += spread * Random.value - spread / 2;

            ItemSpawnManager.Instance.SpawnItem(positionOfLog, item, amountPerDrop);
        }
        
        Destroy(gameObject);
    }
}
