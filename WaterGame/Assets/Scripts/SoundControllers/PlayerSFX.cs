using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{

    [Header("Sources")]
    public AudioSource jump;
    public AudioSource doubleJump;
    public AudioSource dash;
    

    public void PlaySound(string clipName)
    {
        switch (clipName)
        {
            case "jump":
                jump.Play();
                break;

            case "doubleJump":
                doubleJump.Play();
                break;

            case "dash":
                dash.Play();
                break;

            default:
                return;

        }

    }
}
