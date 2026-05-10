using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private MovementComponent movementComponent;

    private InputAction moveAction;
    private InputAction jumpAction;

    private Vector2 moveInput;

    void Start()
    {
        movementComponent = GetComponent<MovementComponent>();

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        movementComponent.Move(moveInput);
        HandleJump();
    }

    void HandleMovement()
    {
        // Read the movement input from the Input System
        moveInput = moveAction.ReadValue<Vector2>();
    }

    void HandleJump()
    {
        if (jumpAction.triggered)
        {
            movementComponent.Jump();
        }
    }
}
