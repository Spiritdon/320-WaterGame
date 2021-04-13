using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform influenceDirection;
    [SerializeField] private TriggerEvent colTrigger;

    [Header("Parameters")]
    [SerializeField] float fanForce = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        colTrigger.triggerEvent += TriggeredEvent;
    }

    private void OnDestroy()
    {
        colTrigger.triggerEvent -= TriggeredEvent;
    }

    void TriggeredEvent(TriggerState triggerState, Collider other)
    {
        if(triggerState != TriggerState.Exit)
        {
            if (other.CompareTag("Player") && PlayerState.currentPlayerState == PlayerMatterState.CLOUD)
            {
                PlayerMovement pm = other.gameObject.GetComponent<PlayerMovement>();

                Vector3 influenceVec = influenceDirection.transform.forward * fanForce;
                
                //Influence movement of the player
                pm.InfluenceVelocity(influenceVec);
            }
        }

    }
}
