using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    private float jumpForce;
    private float playerSpeed;
    private float movementX;
    private float movementZ;
    private Vector3 playerMovement;
    void Start()
    {
       
        playerSpeed = 5.0f;
        jumpForce = 5.0f;
        //transform.position = new Vector3(10,3,10);
    }

    // Update is called once per frame
    void Update()
    {
        movementX = Input.GetAxis("Horizontal");
        movementZ = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0,jumpForce,0),ForceMode.Impulse);
        }
        transform.position += new Vector3(movementX,0,movementZ)*playerSpeed*Time.deltaTime;
    }
}
