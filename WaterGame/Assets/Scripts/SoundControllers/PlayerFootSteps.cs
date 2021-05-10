using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSteps : MonoBehaviour
{

    [Header("Feet")]
    public AudioSource leftFoot;
    public AudioSource rightFoot;
    // Start is called before the first frame update

    public void PlayFootStep(string foot)
    { 
        if(foot == "left")
        {
            if (leftFoot.isPlaying)
            {
                leftFoot.Stop();
            }
            leftFoot.pitch = Random.Range(0.8f, 1.2f);
            leftFoot.Play();
        }
        else if (foot == "right")
        {
            if (rightFoot.isPlaying)
            {
                rightFoot.Stop();
            }
            rightFoot.pitch = Random.Range(0.8f, 1.2f);
            rightFoot.Play();
        }
    }
}
