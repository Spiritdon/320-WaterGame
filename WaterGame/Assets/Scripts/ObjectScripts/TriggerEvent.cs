using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TriggerState { Enter,Exit,Stay}
public class TriggerEvent : MonoBehaviour
{
    public delegate void Trigger(TriggerState triggerState, Collider other);
    public Trigger triggerEvent;



    private void OnTriggerEnter(Collider other)
    {
        triggerEvent?.Invoke(TriggerState.Enter, other);
    }

    private void OnTriggerExit(Collider other)
    {
        triggerEvent?.Invoke(TriggerState.Exit, other);
    }

    private void OnTriggerStay(Collider other)
    {
        //Limit to player interaction
        if(other.CompareTag("Player"))
        {
            triggerEvent?.Invoke(TriggerState.Stay, other);
        }
       
    }
}
