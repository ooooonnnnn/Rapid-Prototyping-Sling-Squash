using UnityEngine;

public class DetectHit : MonoBehaviour
{
    [SerializeField] OnTriggerEvent outerTrig;
    [SerializeField] OnTriggerEvent innerTrig;
    [SerializeField] private RemoveTarget reTarget;

    [SerializeField] private Timer timer;
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
            timer?.StartTimer();
            print("Perfect Hit!");
            StartTargetMovement();// when hit for the first time, start moving the target on the y axis
            reTarget.MakeInvisible();
            return;
        }
        if (ballInOuter)
        {
            ScoreManager.Instance.AddScore(5);
            print("Hit!");
            reTarget.MakeInvisible();
            return;
        }
    }
    
    private void StartTargetMovement()
    {
        GetComponent<TargetMovement>().StartMoving();
    }
    
}
