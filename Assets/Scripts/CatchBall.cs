using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(DistanceJoint2D))]
public class CatchBall : MonoBehaviour
{
    [SerializeField] private GameObject ball;

    private bool isGrabbing = false;
    [SerializeField] private RemoveTarget reTarget;
    public event Action OnGrab;

    private InputSystem_Actions inputActions;

    [SerializeField, HideInInspector] private DistanceJoint2D joint;
    [SerializeField, HideInInspector] private float maxDistance;
    
    [Header("Catch Feedback")]
    [SerializeField] private SpriteRenderer playerSR;
    [SerializeField] private Color catchColor = Color.green;
    private Color _originalColor;

    private void OnValidate()
    {
        joint = GetComponent<DistanceJoint2D>();
        if (ball != null)
        {
            var rb = ball.GetComponent<Rigidbody2D>();
            if (rb != null) joint.connectedBody = rb;
        }
        maxDistance = joint.distance;
    }

    private void Awake()
    {
        if (!joint) joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;

        _originalColor = playerSR.color;
        
        inputActions = new InputSystem_Actions();
        // Do NOT call inputActions.Enable() here (would enable all maps incl. UI).
    }

    private void OnEnable()
    {
        // Subscribe first, then enable just the Player map.
        inputActions.Player.Attack.performed += TryGrab;
        inputActions.Player.Attack.canceled += StopGrab;
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        // Unsubscribe and disable exactly what we enabled.
        inputActions.Player.Attack.performed -= TryGrab;
        inputActions.Player.Attack.canceled -= StopGrab;
        inputActions.Player.Disable();
    }

    private void OnDestroy()
    {
        // Clean up to avoid finalizer warnings/leaks.
        inputActions?.Dispose();
        OnGrab = null;
    }

    private void TryGrab(InputAction.CallbackContext _)
    {
        isGrabbing = true;
    }

    private void StopGrab(InputAction.CallbackContext _)
    {
        isGrabbing = false;
        
        // revert color when releasing the grab
        playerSR.color = _originalColor;
    }

    private void FixedUpdate()
    {
        if (joint.enabled == isGrabbing) return;

        if (!isGrabbing)
        {
            joint.enabled = false;
            return;
        }

        if (ball == null) return;

        float distance = Vector2.Distance(transform.position, ball.transform.position);
        if (distance <= maxDistance)
        {
            playerSR.color = catchColor;
            joint.enabled = true;
            OnGrab?.Invoke();
            reTarget.MakeVisible();
        }
    }
}
