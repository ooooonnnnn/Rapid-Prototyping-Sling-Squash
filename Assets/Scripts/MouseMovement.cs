using UnityEngine;
using UnityEngine.InputSystem;

public class MouseMovement : MonoBehaviour
{
    //controls the velocity of the object with the mouse
    [SerializeField] float sensitivity;
    [SerializeField, HideInInspector] Rigidbody2D rb;
    private InputSystem_Actions inputActions;
    private Vector2 targetVelocity;
    private float lastMouseMoveTime;

    private void OnValidate()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        inputActions = new();
        inputActions.Enable();
        lastMouseMoveTime = Time.time;
    }

    private void OnEnable()
    {
        inputActions.Player.Look.performed += Look_performed;
    }

    private void OnDisable()
    {
        inputActions.Player.Look.performed -= Look_performed;
    }

    private Vector2 mouseDelta;
    private void Look_performed(InputAction.CallbackContext input)
    {
        mouseDelta += input.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        targetVelocity = mouseDelta / Time.fixedDeltaTime * sensitivity;
        rb.linearVelocity = targetVelocity;
        mouseDelta = Vector2.zero;
    }
}
