using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private DetectHit currentTarget;
    public static Vector2 impactPos;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        impactPos = collision.GetContact(0).point;
        currentTarget?.TryHit();
    }
}
