using UnityEngine;

public class FireflyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = .2f;
    [SerializeField] private float frequency = .2f;
    [SerializeField] private float magnitude = .5f;
    [SerializeField] private float timeChangeMin = 2f;
    [SerializeField] private float timeChangeMax = 15f;

    [SerializeField] private int floatDirection;

    private Vector2 movePos;
    private Vector2 startPos;

    private float currentTime;
    private float nextDirectionChange;

    void Start()
    {
        startPos = this.transform.position;
    }

    void OnEnable()
    {
        nextDirectionChange = Random.Range(timeChangeMin, timeChangeMax);
    }

    void Update()
    {
        CheckIfDirectionChange();
        Move();
    }

    private void CheckIfDirectionChange()
    {
        currentTime += Time.deltaTime;

        if (currentTime > nextDirectionChange)
        {
            floatDirection *= -1;

            currentTime = 0;
            nextDirectionChange = Random.Range(timeChangeMin, timeChangeMax);
            this.transform.Rotate(Vector2.zero, Random.Range(0, 90));
        }
    }

    private void Move()
    {
        movePos.y = startPos.y + Mathf.Sin(Time.time * frequency) * magnitude;
        if (floatDirection > 0)
        {
            movePos.x += Time.deltaTime * moveSpeed;
        }

        if (floatDirection < 0)
        {
            movePos.x -= Time.deltaTime * moveSpeed;
        }

        transform.position = new Vector2(movePos.x, movePos.y);
    }
}
