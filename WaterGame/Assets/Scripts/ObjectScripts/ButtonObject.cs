using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObject : MonoBehaviour
{
    bool activated = false;
    bool playerOnButton = false;
    
    [SerializeField]
    [Tooltip("How long this button stays activated after its pressed")]
    float holdTime = 0;

    float holdTimeElapsed = 0;

    //References
    [SerializeField] GameObject buttonTop;
    [SerializeField] private TriggerEvent colTrigger;
    [SerializeField] AudioClip soundClip;
    [SerializeField] AudioSource audioSource;

    //Delegates

    public delegate void ButtonEvent();
    public ButtonEvent buttonActivated;
    public ButtonEvent buttonReleased;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        colTrigger.triggerEvent += TriggeredEvent;
    }
    private void OnDestroy()
    {
        colTrigger.triggerEvent -= TriggeredEvent;
    }

    void TriggeredEvent(TriggerState triggerState, Collider other)
    {
        if (triggerState != TriggerState.Exit)
        {
            if (other.CompareTag("Player") && PlayerState.currentPlayerState == PlayerMatterState.ICE)
            {
                if (other.GetComponent<PlayerMovement>().GroundPounding)
                {
                    if(!activated)
                    {
                        playerOnButton = true;
                        ActivateButton();
                       
                    }
                   
                }
              
            }
        }
        else
        {
            playerOnButton = false;
            if(holdTime == 0)
            {
                ReleaseButton();
                
            }
        }
    }

    void ActivateButton()
    {
        activated = true;
        buttonActivated?.Invoke();  //Call delegate
        buttonTop.transform.localPosition -= new Vector3(0, 0, 0.0112f);
        audioSource.PlayOneShot(soundClip);
        if (holdTime != 0)
        {
            StartCoroutine(HoldActivated());
        }
    }

    void ReleaseButton()
    {
        if(activated)
        {
            activated = false;
            buttonReleased?.Invoke();  //Call delegate
            buttonTop.transform.localPosition += new Vector3(0, 0, 0.0112f);
            audioSource.PlayOneShot(soundClip);
        }
      
    }

    IEnumerator HoldActivated()
    {
        //Wait until player gets off the button
        while(playerOnButton)
        {
            yield return new WaitForEndOfFrame();
        }

        //Release button after time held
        yield return new WaitForSeconds(holdTime);

        ReleaseButton();
    }
}
