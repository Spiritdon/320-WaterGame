using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerStates { Enter, Exit}
public class TriggerEventTramp : MonoBehaviour
{
    public delegate void Trigger(TriggerStates triggerState, Collider other);
    public Trigger triggerEvents;

    private void OnTriggerEnter(Collider other)
    {
        triggerEvents?.Invoke(TriggerStates.Enter, other);
    }

    private void OnTriggerExit(Collider other)
    {
        triggerEvents?.Invoke(TriggerStates.Exit, other);
    }

}
