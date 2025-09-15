using TMPro;
using UnityEngine;

public class DetectHit : MonoBehaviour
{
    [Header("Main Target References")] [SerializeField]
    private OnTriggerEvent outerTrig;

    [SerializeField] private OnTriggerEvent innerTrig;

    [Header("Spawned Target Reference")] [SerializeField]
    private OnTriggerEvent spawnTrig;

    [Header("Common References")] [SerializeField]
    private RemoveTarget reTarget;

    [SerializeField] private Timer timer;

    private bool ballInInner = false;
    private bool ballInOuter = false;
    private bool ballInSpawn = false;
    private bool isSpawnedTarget = false;
    private static int timesHit;

    public GameObject impactScoreTextPrefab;

    [SerializeField] private int outerTargetScore = 5;
    [SerializeField] private int innerTargetScore = 10;

    private void Awake()
    {
        
        isSpawnedTarget = (spawnTrig != null);
        if (isSpawnedTarget)
        {
            Debug.Log("Setting up as spawned target");
            
            if (spawnTrig != null)
            {
                spawnTrig.onTriggerEnter += _ =>
                {
                    ballInSpawn = true;
                    Debug.Log("Spawn trigger entered!");
                    HitSpawnTarget(); // removes the spawned target
                };
                spawnTrig.onTriggerExit += _ =>
                {
                    ballInSpawn = false;
                    Debug.Log("Spawn trigger exited!");
                };
            }
        }
        else
        {
            Debug.Log("Setting up as main target");
            if (outerTrig != null)
            {
                outerTrig.onTriggerEnter += _ => ballInOuter = true;
                outerTrig.onTriggerExit += _ => ballInOuter = false;
            }

            if (innerTrig != null)
            {
                innerTrig.onTriggerEnter += _ => ballInInner = true;
                innerTrig.onTriggerExit += _ => ballInInner = false;
            }
        }
        
        if (reTarget == null)
            reTarget = GetComponent<RemoveTarget>();

        Debug.Log($"{gameObject.name} reTarget found: {reTarget != null}");
    }

    public void ResetHitState()
    {
        ballInInner = false;
        ballInOuter = false;

        Debug.Log("Hit state reset");
    }

    public void TryHit()
    {

        // Handle main target hit
        if (ballInInner)
        {
            ScoreManager.Instance.AddScore(10);
            GameObject impactScoreText =
                Instantiate(impactScoreTextPrefab, TargetManager.impactPos, Quaternion.identity);
            impactScoreText.GetComponent<TextMeshPro>().text = "+" + innerTargetScore;
            timer?.StartTimer();
            Debug.Log("Perfect Hit!");
            StartTargetMovement();
            reTarget?.MakeInvisible();
            timesHit++;
            return;
        }

        if (ballInOuter)
        {
            ScoreManager.Instance.AddScore(5);
            Debug.Log("Hit!");
            reTarget?.MakeInvisible();
            timesHit++;
            GameObject impactScoreText =
                Instantiate(impactScoreTextPrefab, TargetManager.impactPos, Quaternion.identity);
            impactScoreText.GetComponent<TextMeshPro>().text = "+" + outerTargetScore;

            return;
        }

        Debug.Log("No main target hit detected");
    }


    private void StartTargetMovement()
    {
        TargetMovement movement = GetComponent<TargetMovement>();
        if (movement != null)
        {
            movement.StartMoving();
        }
    }

    private void HitSpawnTarget()
    {
        Debug.Log("Hit spawn target!");
        ScoreManager.Instance.AddScore(5);
        reTarget?.MakeInvisible(); 
        Debug.Log("MakeInvisible called");
        timesHit++;
        return;
    }

    public static int TimesHit()
    {
        return timesHit;
    }

    public static void ResetHits()
    {
        timesHit = 0;
    }
    
    
    // if both targets are hit before "reloading" (player holding the ball) then the player gets extra points
}
