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
            print("Perfect Hit!");
            return;
        }
        if (ballInOuter)
        {
            print("Hit!");
            return;
        }
    }
}
