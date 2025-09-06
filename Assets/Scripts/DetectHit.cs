using UnityEngine;

public class DetectHit : MonoBehaviour
{
    [SerializeField] OnTriggerEvent outerTrig;
    [SerializeField] OnTriggerEvent innerTrig;

    private bool ballInInner = false;
    private bool ballInOuter = false;

    private void Awake()
    {
        outerTrig.onTriggerEnter += _ => ballInOuter = true;
        outerTrig.onTriggerExit += _ => ballInOuter = false;
        innerTrig.onTriggerEnter += _ => ballInInner = true;
        innerTrig.onTriggerExit += _ => ballInInner = false;
    }

    public void TryHit()
    {
        if (ballInInner)
        {
            ScoreManager.Instance.AddScore(10);
            print("Perfect Hit!");
            // when hit for the first time, start moving the target on the y axis
            StartTargetMovement();
            return;
        }
        if (ballInOuter)
        {
            ScoreManager.Instance.AddScore(5);
            print("Hit!");
            return;
        }
    }
    
    private void StartTargetMovement()
    {
        GetComponent<TargetMovement>().StartMoving();
    }
}
