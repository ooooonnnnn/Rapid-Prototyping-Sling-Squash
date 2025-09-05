using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private DetectHit currentTarget;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentTarget?.TryHit();
    }
}
