using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]
public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction toggleRunAction;

    [SerializeField] public float WalkSpeed = 2f;
    [SerializeField] public float RunSpeed = 4f;

    [SerializeField] public float CurrentSpeed;

    private Vector2 motionVector;
    public  Vector2 LastDirection;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        toggleRunAction = playerInput.actions["ToggleRun"];
    }

    void OnEnable()
    {
        moveAction.performed += MoveStart;
        moveAction.canceled += MoveStop;

        toggleRunAction.performed += RunStart;
        toggleRunAction.canceled += RunStop;

        CurrentSpeed = WalkSpeed;
    }

    void OnDisable()
    {
        moveAction.performed -= MoveStart;
        moveAction.canceled -= MoveStop;

        toggleRunAction.performed -= RunStart;
        toggleRunAction.canceled -= RunStop;
    }

    private void RunStop(InputAction.CallbackContext obj)
    {
        CurrentSpeed = WalkSpeed;
        animator.speed = 1;
    }

    private void RunStart(InputAction.CallbackContext obj)
    {
        CurrentSpeed = RunSpeed;
        animator.speed = 2f;
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

    public void MoveStart(InputAction.CallbackContext context)
    {
        motionVector = context.ReadValue<Vector2>();
    }

    private void MoveStop(InputAction.CallbackContext context)
    {
        LastDirection = motionVector;
        motionVector = new Vector2(0, 0);
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
