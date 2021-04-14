using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampInteraction : MonoBehaviour
{
    private float tramForce;
    private TriggerEvent triggerEvent;
    // Start is called before the first frame update
    void Start()
    {
        tramForce = 10;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.other.CompareTag("Player") && PlayerState.currentPlayerState == PlayerMatterState.ICE)
        {
            Rigidbody playerRB = collision.other.GetComponent<Rigidbody>();
            if (tramForce<25)
            {
                tramForce += Mathf.Abs(playerRB.velocity.y);
            }
            print(tramForce);
            playerRB.AddForce(Vector3.up * tramForce, ForceMode.Impulse);
        }
    }
}
