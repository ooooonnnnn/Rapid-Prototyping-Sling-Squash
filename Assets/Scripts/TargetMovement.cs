using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveDistance = 3f;
    
    private Vector3 startPosition;
    private bool shouldMove = false;


    private void Start()
    {
        startPosition = transform.position;
    }

    public void StartMoving()
    {
        shouldMove = true;
    }

    private void Update()
    {
        if (shouldMove)
        {
            float newY = startPosition.y + Mathf.Sin(Time.time * moveSpeed) * moveDistance;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}

