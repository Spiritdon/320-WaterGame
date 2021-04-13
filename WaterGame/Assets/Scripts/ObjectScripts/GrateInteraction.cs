using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrateInteraction : MonoBehaviour
{

    public TriggerEvent triggerCol;
    public Collider grateCol;
    // Start is called before the first frame update
    void Start()
    {
        triggerCol.triggerEvent += TriggeredEvent;
    }

    private void OnDestroy()
    {
        triggerCol.triggerEvent -= TriggeredEvent;
    }

    //Unity's component system is wack, cant use ontriggerEnter from other collider object :,(
    void TriggeredEvent(bool entered,Collider other)
    {
        if (entered)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (PlayerState.currentPlayerState == PlayerMatterState.DROP)
                {
                    grateCol.enabled = false;
                }
                else
                {
                    grateCol.enabled = true;
                }
            }
            else
            {
                grateCol.enabled = true;
            }
        }
        else
        {
            //ReEnable collision
            if (other.gameObject.CompareTag("Player"))
            {
                 grateCol.enabled = true;
            }

        }
        
    }
}
