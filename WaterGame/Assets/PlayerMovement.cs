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
    private Rigidbody rb;

    private BoxCollider collider;
    private int extraJumpCounter;

    //Animation Properties
    public Animator anim;
    public GameObject visObject;
  
    
    void Start()
    {
        extraJumpCounter = 1;
        playerSpeed = 5.0f;
        jumpForce = 5.0f;
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
        //transform.position = new Vector3(10,3,10);
    }
    private bool IsGrounded()//checks if the player is touching the ground via raycasting
    {
        float distanceToGround = collider.bounds.extents.y;
        return Physics.Raycast(transform.position, -Vector3.up,distanceToGround +0.1f);
        ///for this to return true their must be a intercetion between the ray and the ground 
        ///because the box collider can prevent a intercection(distance can be zero and is therefore not interceting)
        ///so the +.1f ensures when the player lands their is a intercetion   
    }
    // Update is called once per frame
    void Update()
    {
        //basic player movement
        movementX = Input.GetAxis("Horizontal");
        movementZ = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(movementX, 0.0f, movementZ);

        //code that determines if the player has double jumped
        if (IsGrounded())//if the player is touching the ground replenish his double jump
        {
            extraJumpCounter = 1;
        }
        if (Input.GetKeyDown(KeyCode.Space)&& extraJumpCounter != 0)//allow the player to jump as long as they are pressing space and they have extra jumps
        {
            extraJumpCounter--;
            rb.AddForce(new Vector3(0,jumpForce,0),ForceMode.Impulse);
        }

        transform.position += new Vector3(movementX,0,movementZ)*playerSpeed*Time.deltaTime;

        //Set animator floats
        anim.SetFloat("HorizSpeed", input.magnitude);
        anim.SetFloat("VertSpeed", rb.velocity.y);

        //If moving, update rotation angle (TEMP)
        if (input.magnitude != 0.0f)
        {
            float vertRot = Mathf.Atan2(movementX, movementZ);

            visObject.transform.eulerAngles = new Vector3(visObject.transform.eulerAngles.x,
                Mathf.LerpAngle(visObject.transform.eulerAngles.y, Mathf.Rad2Deg * vertRot, Time.deltaTime * 15f)
                , visObject.transform.eulerAngles.z);
        }


        //Check if grounded
        if(rb.velocity.y == 0.0f)
        {
            anim.SetBool("Grounded", true);
        }
        else
        {
            anim.SetBool("Grounded", false);
        }
    }
}
