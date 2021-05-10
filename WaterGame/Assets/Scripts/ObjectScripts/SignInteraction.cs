using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignInteraction : MonoBehaviour
{
    public Collider player;
    public TriggerEvent triggerEvent;
    public BoxCollider trigger;
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        triggerEvent.triggerEvent += SignTrigger;
    }
    void OnDestroy()
    {
        triggerEvent.triggerEvent -= SignTrigger;
    }

    void SignTrigger(TriggerState triggerState, Collider other)
    {
        if (triggerState != TriggerState.Exit)
        {
            if (signTriggerHit())
            {
                panel.SetActive(true);
            }
        }
        else
        {
            panel.SetActive(false);
        }
    }

    bool signTriggerHit()
    {
        return player.bounds.Intersects(trigger.bounds);
    }

}
