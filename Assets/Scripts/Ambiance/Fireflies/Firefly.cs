using Assets.Scripts.Ambiance;
using UnityEngine;

[RequireComponent(typeof(RandomLight))]
public class Firefly : MonoBehaviour
{
    [SerializeField] private float moveSpeed = .2f;
    [SerializeField] private float frequency = .2f;
    [SerializeField] private float magnitude = .5f;
    [SerializeField] private float timeChangeMin = 2f;
    [SerializeField] private float timeChangeMax = 15f;

    [SerializeField] private int floatDirection;

    private RandomLight buttLight;

    private Vector2 movePos;
    private Vector2 startPos;

    private float currentTime;
    private float nextDirectionChange;

    private bool isTurningOff;

    public bool IsToggling => buttLight.IsToggling;

    void Awake()
    {
        startPos = this.transform.position;
        movePos = startPos;

        buttLight = GetComponent<RandomLight>();
    }

    void Start()
    {
        FireflyController.Instance.AddFirefly(this);
    }

    void OnEnable()
    {
        nextDirectionChange = Random.Range(timeChangeMin, timeChangeMax);
    }

    void Update()
    {
        CheckIfDirectionChange();
        Move();

        if (isTurningOff)
        {
            if (!buttLight.IsOn)
            {
                isTurningOff = false;
                this.gameObject.SetActive(false);
            }
        }
    }

    private void CheckIfDirectionChange()
    {
        currentTime += Time.deltaTime;

        if (currentTime > nextDirectionChange)
        {
            floatDirection *= -1;

            currentTime = 0;
            nextDirectionChange = Random.Range(timeChangeMin, timeChangeMax);
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

    public void TurnOn(bool randomize)
    {
        this.gameObject.SetActive(true);
        this.buttLight.TurnOn(randomize);
    }

    public void TurnOff(bool randomize)
    {
        this.buttLight.TurnOff(randomize);
        isTurningOff = true;
    }
}
