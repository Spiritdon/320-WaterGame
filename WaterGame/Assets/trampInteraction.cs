using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampInteraction : MonoBehaviour
{
    private float tramForce;
    private TriggerEvent triggerEvent;
    public PlayerMovement movement;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        tramForce = 25;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (movement.IsTrampHit())
        {
            Rigidbody playerRB = player.GetComponent<Rigidbody>();
            tramForce = 25;
            print(tramForce);
            playerRB.AddForce(Vector3.up * tramForce, ForceMode.Impulse);
        }*/
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.other.CompareTag("Player") && PlayerState.currentPlayerState == PlayerMatterState.ICE)
        {
            Rigidbody playerRB = collision.other.GetComponent<Rigidbody>();
            //tramForce += Mathf.Abs(playerRB.velocity.y);
            if (tramForce<25)
            {

            }
            else if(tramForce >= 25)
            {
                tramForce = 25;
            }
            tramForce = 25;
            print(tramForce);
            playerRB.AddForce(Vector3.up * tramForce, ForceMode.Impulse);
        }
        

    }*/
}
