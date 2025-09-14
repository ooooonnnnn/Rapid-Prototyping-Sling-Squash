using TMPro;
using UnityEngine;

public class DetectHit : MonoBehaviour
{
    [SerializeField] OnTriggerEvent outerTrig;
    [SerializeField] OnTriggerEvent innerTrig;
    [SerializeField] private RemoveTarget reTarget;

    [SerializeField] private Timer timer;
    private bool ballInInner = false;
    private bool ballInOuter = false;

    public GameObject impactScoreTextPrefab;

    [SerializeField] private int outerTargetScore = 5;
    [SerializeField] private int innerTargetScore = 10;

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
            GameObject impactScoreText =
                Instantiate(impactScoreTextPrefab, TargetManager.impactPos, Quaternion.identity);
            impactScoreText.GetComponent<TextMeshPro>().text = "+" + innerTargetScore;
            timer?.StartTimer();
            print("Perfect Hit!");
            StartTargetMovement();// when hit for the first time, start moving the target on the y axis
            reTarget.MakeInvisible();
            return;
        }
        if (ballInOuter)
        {
            ScoreManager.Instance.AddScore(5);
            GameObject impactScoreText =
                Instantiate(impactScoreTextPrefab, TargetManager.impactPos, Quaternion.identity);
            impactScoreText.GetComponent<TextMeshPro>().text = "+" + outerTargetScore;
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
