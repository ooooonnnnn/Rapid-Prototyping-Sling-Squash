using UnityEngine;

public class PooledTarget : MonoBehaviour
{
    private TargetSpawner spawner;
    private bool isUpTarget;
    private DetectHit detectHit;

    public bool IsUpTarget => isUpTarget; //public property

    public void Initialize(TargetSpawner targetSpawner, bool upTarget)
    {
        spawner = targetSpawner;
        isUpTarget = upTarget;
        detectHit = GetComponent<DetectHit>();
    }
    
    public void OnSpawn()
    {
        // Reset the DetectHit state
        if (detectHit != null)
        {
            detectHit.ResetHitState();
        }
        
        // Make sure the target is visible and interactive
        RemoveTarget removeTarget = GetComponent<RemoveTarget>();
        if (removeTarget != null)
        {
            removeTarget.MakeVisible();
        }
    }

    public void ReturnToPool()
    {
        if (spawner != null)
        {
            spawner.ReturnTargetToPool(gameObject, isUpTarget);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
