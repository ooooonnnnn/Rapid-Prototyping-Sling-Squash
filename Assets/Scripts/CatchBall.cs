using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatchBall : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    private bool isGrabbing = false;
    public event Action OnGrab;

    private InputSystem_Actions inputActions;

    [SerializeField, HideInInspector] private DistanceJoint2D joint;
    [SerializeField, HideInInspector] private float maxDistance;

    private void OnValidate()
    {
        joint = GetComponent<DistanceJoint2D>();
        joint.connectedBody = ball.GetComponent<Rigidbody2D>();
        maxDistance = joint.distance;
    }

    void Awake()
    {
        inputActions = new();
        inputActions.Enable();
        joint.enabled = false;
    }

    private void OnEnable()
    {
        inputActions.Player.Attack.performed += TryGrab;
        inputActions.Player.Attack.canceled += StopGrab;
    }

    private void OnDisable()
    {
        inputActions.Player.Attack.performed -= TryGrab;
        inputActions.Player.Attack.canceled -= StopGrab;
    }

    private void TryGrab (InputAction.CallbackContext context)
    {
        isGrabbing = true;
    }

    private void StopGrab(InputAction.CallbackContext context)
    {
        isGrabbing = false;
    }

    private void FixedUpdate()
    {
        if (joint.enabled == isGrabbing) 
            return;

        if (!isGrabbing)
        {
            joint.enabled = false;
            return;
        }

        float distance = Vector2.Distance(transform.position, ball.transform.position);
        if (distance <= maxDistance)
        {
            joint.enabled = true;
            OnGrab?.Invoke();
        }
    }

    private void OnDestroy()
    {
        OnGrab = null;
    }
}
