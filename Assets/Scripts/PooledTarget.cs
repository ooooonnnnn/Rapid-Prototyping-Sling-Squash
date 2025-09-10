using UnityEngine;

public class PooledTarget : MonoBehaviour
{
    private TargetSpawner spawner;
    private bool isUpTarget;

    public bool IsUpTarget => isUpTarget; //public property

    public void Initialize(TargetSpawner targetSpawner, bool upTarget)
    {
        spawner = targetSpawner;
        isUpTarget = upTarget;
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
