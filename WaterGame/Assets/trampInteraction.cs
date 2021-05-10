using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampInteraction : MonoBehaviour
{
    private float tramForce;
    //public TriggerEvent triggerEvent;
    public TriggerEventTramp triggerEventTramp;
    public PlayerMovement movement;
    public Rigidbody player;
    bool trampActive;
    //float trampTimer;

    // Start is called before the first frame update
    void Start()
    {
        //trampTimer = 0.5f;
        tramForce = 25;
        triggerEventTramp.triggerEvents += TriggerEventTramp;
    }
    void OnDestroy()
    {
        triggerEventTramp.triggerEvents -= TriggerEventTramp;
    }

    void TriggerEventTramp(TriggerStates triggerState, Collider other)
    {
        if (triggerState != TriggerStates.Exit)
        {
            if (other.CompareTag("Player") && PlayerState.currentPlayerState == PlayerMatterState.ICE)
            {
                if(player == null)
                {
                    player = other.GetComponent<Rigidbody>();
                }
                if (other.GetComponent<PlayerMovement>().IsTrampHit())
                {
                    if (!trampActive)
                    {
                        trampActive = true;
                        Bounce();
                    }

                }

            }
        }
        else
        {
            trampActive = false;
        }
    }
    void Bounce()
    {
        tramForce = 25;
        player.AddForce(transform.up * tramForce, ForceMode.Impulse);
    }
}
