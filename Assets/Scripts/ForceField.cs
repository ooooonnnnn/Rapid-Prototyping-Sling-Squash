using UnityEngine;

public class ForceField : MonoBehaviour
{
    [SerializeField] private Vector2 force;
    [SerializeField] private float maxVelocity;
    private void OnCollisionStay2D(Collision2D coll) 
    { 
        Rigidbody2D rb = coll.collider.attachedRigidbody;
        if (rb.linearVelocity.magnitude <= maxVelocity)
            coll.collider.attachedRigidbody.AddForce(force);
    }
}
