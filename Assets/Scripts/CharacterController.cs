using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]
public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private Animator animator;

    [SerializeField] public float Speed = 2f;

    private Vector2 motionVector;
    public  Vector2 LastDirection;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (motionVector != Vector2.zero)
        {
            animator.Play("MovementTree");
            animator.SetFloat("Horizontal", motionVector.x);
            animator.SetFloat("Vertical", motionVector.y);
        }
        else
        {
            animator.Play("IdleTree");
            animator.SetFloat("Horizontal", LastDirection.x);
            animator.SetFloat("Vertical", LastDirection.y);
        }
    }

    void OnMove(InputValue input)
    {
        if (motionVector != Vector2.zero)
        {
            LastDirection = motionVector;
        }

        motionVector = input.Get<Vector2>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigidbody2d.velocity = motionVector * Speed;
    }
}
