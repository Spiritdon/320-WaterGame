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
    public Rigidbody rb;


    //Animation Properties
    public Animator anim;
    public GameObject visObject;
  

    void Start()
    {  
        playerSpeed = 5.0f;
        jumpForce = 5.0f;
        rb = GetComponent<Rigidbody>();
        //transform.position = new Vector3(10,3,10);
    }

    // Update is called once per frame
    void Update()
    {
        movementX = Input.GetAxis("Horizontal");
        movementZ = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(movementX, 0.0f, movementZ);

        if (Input.GetKeyDown(KeyCode.Space))
        {
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
