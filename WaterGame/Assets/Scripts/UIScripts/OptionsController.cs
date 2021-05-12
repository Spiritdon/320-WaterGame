using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsController : MonoBehaviour
{
    public AudioMixer masterMixer;

    public void ChangeMasterVolume(float vol)
    {
        float finalVol = Mathf.Lerp(-80, 0.0f, vol);
        masterMixer.SetFloat("Master_Vol", finalVol);
    }

    public void ChangeMusicVolume(float vol)
    {
        float finalVol = Mathf.Lerp(-80.0f, 0f, vol);
        masterMixer.SetFloat("Music_Vol", finalVol);
    }
}
