using UnityEngine;

public class TimeBasedItemSpawner : TimeBasedBehaviorBase
{
    [SerializeField] private Item itemToSpawn;
    [SerializeField] private int numberToSpawn;
    [SerializeField] private float spread = 2f;
    [SerializeField] private float probability = .5f;
    [SerializeField] private float intervalPhases = 4; // per hour

    protected override void HandlePhaseStarted(int phase)
    {
        if (phase % intervalPhases == 0)
        {
            if (Random.value < probability)
            {
                for (int i = 0; i < numberToSpawn; i++)
                {
                    Vector2 positionOfResourceSource = transform.position;

                    positionOfResourceSource.x += spread * Random.value - spread / 2;
                    positionOfResourceSource.y += spread * Random.value - spread / 2;

                    ItemSpawnManager.Instance.SpawnItem(positionOfResourceSource, itemToSpawn, 1);
                }
            }
        }
    }
}
