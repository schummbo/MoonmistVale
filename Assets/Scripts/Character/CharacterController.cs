using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] public float WalkSpeed = 2f;
    [SerializeField] public float RunSpeed = 4f;

    public Vector2 MotionVector;

    private Rigidbody2D rigidbody2d;
    private Animator animator;

    private float currentSpeed;
    private bool isMoving;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        currentSpeed = WalkSpeed;
    }

    public void OnToggleRun(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            currentSpeed = RunSpeed;
            animator.speed = 2f;
        }

        if (obj.canceled)
        {
            currentSpeed = WalkSpeed;
            animator.speed = 1;
        }
    }

    void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        animator.SetFloat("Horizontal", MotionVector.x);
        animator.SetFloat("Vertical", MotionVector.y);
        animator.SetBool("IsMoving", isMoving);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            MotionVector = context.ReadValue<Vector2>();
            isMoving = true;
        }

        if (context.canceled)
        {
            isMoving = false;
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
            Move();
        else
            Stop();
    }

    private void Move()
    {
        rigidbody2d.velocity = MotionVector * currentSpeed;
    }

    private void Stop()
    {
        rigidbody2d.velocity = Vector2.zero;
    }
}
