using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum PlayerMatterState { DROP,CLOUD,ICE }
public class PlayerState : MonoBehaviour
{
    //Can be accessed from other scripts
    public static PlayerMatterState currentPlayerState = PlayerMatterState.DROP;


    //References
    public GameObject dropModel;
    public GameObject cloudModel;
    public GameObject iceModel;
    public ParticleSystem changeParticle;

    // Start is called before the first frame update
    void Start()
    {
        UpdatePlayerModel();
    }

    // Update is called once per frame
    void Update()
    {

        //Drop
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(currentPlayerState != PlayerMatterState.DROP)
            {
                currentPlayerState = PlayerMatterState.DROP;
                UpdatePlayerModel();
            }
          
        }
        //Ice
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (currentPlayerState != PlayerMatterState.CLOUD)
            {
                currentPlayerState = PlayerMatterState.CLOUD;
                UpdatePlayerModel();
            }
          
        }
        //Cloud
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (currentPlayerState != PlayerMatterState.ICE)
            {
                currentPlayerState = PlayerMatterState.ICE;
                UpdatePlayerModel();
            }
        }

    }


    /// <summary>
    /// Update the player model to match the current state
    /// </summary>
    private void UpdatePlayerModel()
    {
        changeParticle.Play();

        switch (currentPlayerState)
        {
            case PlayerMatterState.CLOUD:
                cloudModel.SetActive(true);
                iceModel.SetActive(false);
                dropModel.SetActive(false);
                break;

            case PlayerMatterState.ICE:
                iceModel.SetActive(true);
                cloudModel.SetActive(false);
                dropModel.SetActive(false);

                break;

            case PlayerMatterState.DROP:
                dropModel.SetActive(true);
                iceModel.SetActive(false);
                cloudModel.SetActive(false);

                break;

            default:
                return;
        }

    }
}
