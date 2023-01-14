using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]
public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private Animator animator;

    [SerializeField] public float WalkSpeed = 2f;
    [SerializeField] public float RunSpeed = 4f;

    [SerializeField] public float CurrentSpeed;

    private Vector2 motionVector;
    public Vector2 LastDirection;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        CurrentSpeed = WalkSpeed;
    }

    public void OnToggleRun(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            CurrentSpeed = RunSpeed;
            animator.speed = 2f;
        }

        if (obj.canceled)
        {
            CurrentSpeed = WalkSpeed;
            animator.speed = 1;
        }
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

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            motionVector = context.ReadValue<Vector2>();
        }

        if (context.canceled)
        {
            LastDirection = motionVector;
            motionVector = new Vector2(0, 0);
        }
    }


    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigidbody2d.velocity = motionVector * CurrentSpeed;
    }
}
