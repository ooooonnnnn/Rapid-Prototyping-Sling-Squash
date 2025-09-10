using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.PlayerLoop;
using Random = System.Random;



public class TargetSpawner : MonoBehaviour
{
    [Header("Target Bounds")] 
    [SerializeField] private Transform upperBounds;

    [SerializeField] private Transform lowerBounds;

    [Header("Target Prefabs")] [SerializeField]
    private GameObject upTarget;

    [SerializeField] private GameObject downTarget;

    [Header("Pool Settings")] [SerializeField]
    private int poolSize = 5;

    private Queue<GameObject> upTargetPool = new Queue<GameObject>();
    private Queue<GameObject> downTargetPool = new Queue<GameObject>();
    private List<GameObject> activeTargets = new List<GameObject>();

    private Random random;

    private void Awake()
    {
        InitializePools();

    }

    private void InitializePools()
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreatePooledTargets(true);
            CreatePooledTargets(false);
        }
    }

    private GameObject CreatePooledTargets(bool isUpTarget)
    {
        GameObject prefab = isUpTarget ? upTarget : downTarget;
        GameObject target = Instantiate(prefab);
        target.SetActive(false);

        PooledTarget pooledTarget = target.GetComponent<PooledTarget>();
        if (pooledTarget == null)
        {
            pooledTarget = target.AddComponent<PooledTarget>();
        }
        pooledTarget.Initialize(this,isUpTarget);

        if (isUpTarget)
        {
            upTargetPool.Enqueue(target);
        }
        else
        {
            downTargetPool.Enqueue(target);
        }

        return target;
    }

    //THIS IS CALLED WHENVER I CATCH THE BALL, CREATES A NEW SET OF TARGETS TO HIT !
    public void SpawnNewTargetSet()
    {
        ClearAllTargets();// clears previous targets

        int spawnTop = UnityEngine.Random.Range(1,3);
        if (spawnTop == 1)
        {
            SpawnTargetFromPool(upperBounds,true,"TargetUp");
        }
        else
        {
            SpawnTargetFromPool(lowerBounds,false,"TargetDown");
        }
    }

    private void SpawnTargetFromPool(Transform bounds, bool isUpTarget, string targetName)
    {
        Queue<GameObject> pool = isUpTarget ? upTargetPool : downTargetPool;
        GameObject target;

        if (pool.Count > 0)
        {
            target = pool.Dequeue();
        }
        else
        {
            target = CreatePooledTargets(isUpTarget);
        }

        target.transform.position = GetRandomPositionInBounds(bounds);
        target.transform.SetParent(bounds);
        target.name = targetName;
        target.SetActive(true);
        
        activeTargets.Add(target);
        
        Debug.Log($"Spawned {targetName} at position: {target.transform.position}");

    }

    private Vector3 GetRandomPositionInBounds(Transform bounds)
    {
        Collider2D boundsCollider = bounds.GetComponent<Collider2D>();

        if (boundsCollider == null)
        {
            return bounds.position;
        }

        Bounds colliderBounds = boundsCollider.bounds;
        float randomX = UnityEngine.Random.Range(colliderBounds.min.x, colliderBounds.max.x);

        return new Vector3(randomX, bounds.position.y, bounds.position.z);
    }

    private void ClearAllTargets()
    {
        foreach (GameObject target in activeTargets)
        {
            if (target != null)
            {
                target.SetActive(false);
                
                PooledTarget pooledTarget = target.GetComponent<PooledTarget>();
                if (pooledTarget != null)
                {
                    if (pooledTarget.IsUpTarget)
                    {
                        upTargetPool.Enqueue(target);
                    }
                    else
                    {
                        downTargetPool.Enqueue(target);
                    }
                }
                
            }
        }
        activeTargets.Clear();
    }
    
    public void ReturnTargetToPool(GameObject target, bool isUpTarget)
    {
        if (target == null) return;

        target.SetActive(false);
        activeTargets.Remove(target);

        if (isUpTarget)
        {
            upTargetPool.Enqueue(target);
        }
        else
        {
            downTargetPool.Enqueue(target);
        }
    }

}
