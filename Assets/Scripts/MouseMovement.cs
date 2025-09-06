using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class MouseMovement : MonoBehaviour
{
    // controls the velocity of the object with the mouse
    [SerializeField] private float sensitivity = 1f;
    [SerializeField, HideInInspector] private Rigidbody2D rb;

    private InputSystem_Actions inputActions;
    private Vector2 mouseDelta;

    private void OnValidate()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        rb = rb ? rb : GetComponent<Rigidbody2D>();
        Cursor.lockState = CursorLockMode.Locked;

        inputActions = new InputSystem_Actions();
        // DO NOT call inputActions.Enable() here (would also enable UI map)
    }

    private void OnEnable()
    {
        inputActions.Player.Look.performed += Look_performed;
        inputActions.Player.Enable();      // enable only the Player map
    }

    private void OnDisable()
    {
        inputActions.Player.Look.performed -= Look_performed;
        inputActions.Player.Disable();     // disable exactly what we enabled
    }

    private void OnDestroy()
    {
        inputActions?.Dispose();           // prevent finalizer warnings/leaks
    }

    private void Look_performed(InputAction.CallbackContext input)
    {
        mouseDelta += input.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = (mouseDelta / Time.fixedDeltaTime) * sensitivity;
        rb.linearVelocity = targetVelocity;   // use rb.velocity on older Unity versions
        mouseDelta = Vector2.zero;
    }
}
