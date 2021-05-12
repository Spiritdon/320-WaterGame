using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsController : MonoBehaviour
{


    public AudioMixer masterMixer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeMasterVolume(float vol)
    {
        float finalVol = Mathf.Lerp(-50, 10f, vol);
        masterMixer.SetFloat("ooga", finalVol);
    }

    public void ChangeMusicVolume(float vol)
    {
        float finalVol = Mathf.Lerp(-50, 10f, vol);
        masterMixer.SetFloat("BG_Music", finalVol);
    }
}
