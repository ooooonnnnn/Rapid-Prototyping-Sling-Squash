using UnityEngine;

public class TestStartVelocity : MonoBehaviour
{
    public float initialV;
    private Rigidbody2D rb;

#if UNITY_EDITOR
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocityX = initialV;
    }

#endif

}
