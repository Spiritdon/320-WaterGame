using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public delegate void Trigger(bool enter, Collider other);
    public Trigger triggerEvent;



    private void OnTriggerEnter(Collider other)
    {
        triggerEvent?.Invoke(true, other);
    }

    private void OnTriggerExit(Collider other)
    {
        triggerEvent?.Invoke(false, other);
    }

    private void OnTriggerStay(Collider other)
    {
        //Limit to player interaction
        if(other.CompareTag("Player"))
        {
            triggerEvent?.Invoke(true, other);
        }
       
    }
}
