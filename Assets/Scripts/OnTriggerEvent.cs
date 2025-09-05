using System;
using UnityEngine;

public class OnTriggerEvent : MonoBehaviour
{
    public event Action<Collider2D> onTriggerEnter;
    public event Action<Collider2D> onTriggerStay;
    public event Action<Collider2D> onTriggerExit;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        onTriggerEnter?.Invoke(coll);
    }
    private void OnTriggerStay2D(Collider2D coll)
    {
        onTriggerStay?.Invoke(coll);
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        onTriggerExit?.Invoke(coll);
    }

    private void OnDestroy()
    {
        onTriggerEnter = null;
        onTriggerStay = null;
        onTriggerExit = null;
    }
}
