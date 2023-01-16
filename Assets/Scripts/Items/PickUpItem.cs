using UnityEngine;

/// <summary>
/// An item that is collectable by the <see cref="player"/>.
/// It will either be destroyed after <see cref="ttl"/> seconds or
/// collected if it is within <see cref="pickUpDistance"/> of player.
/// </summary>
public class PickUpItem : MonoBehaviour
{
    private Transform player;
    [SerializeField] float speed = 5f;
    [SerializeField] float pickUpDistance = 1.5f;
    [SerializeField] float ttl = 10f;
    public Item item;
    public int count = 1;

    void Awake()
    {
        player = GameManager.Instance.Player.transform;
    }

    void Update()
    {
        ttl -= Time.deltaTime;

        if (ttl <= 0)
        {
            Destroy(this.gameObject);
        }

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > pickUpDistance)
        {
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (distance < .1)
        {
            Destroy(this.gameObject);
            GameManager.Instance.InventoryContainer?.Add(item, count);
        }
    }

}