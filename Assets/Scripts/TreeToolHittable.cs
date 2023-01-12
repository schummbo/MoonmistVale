using UnityEngine;

public class TreeToolHittable : ToolHittableItem
{ 
    [SerializeField] GameObject pickUpDrop;
    [SerializeField] int dropCount = 5;
    [SerializeField]  float spread = 0.7f;


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

            var log = Instantiate(pickUpDrop);
            log.transform.position = positionOfLog;
        }
        
        Destroy(gameObject);
    }
}
