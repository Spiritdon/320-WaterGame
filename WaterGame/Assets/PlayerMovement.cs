using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    private float jumpForce;
    private float playerSpeed;
    private float movementX;
    private float movementZ;
    private Rigidbody rb;
    private Vector3 vel;
    Vector3 additionForce = Vector3.zero;
    [SerializeField] float sprintCooldown = 1.0f;
    bool canSprint = true;



    //Is the player Ground pounding
     bool groundPounding = false;
 
    [Header("Player movement attributes")]
    [SerializeField]
    private float iceFriction = 1.0f;
    [SerializeField]
    private float cloudJumpDamper = 1.0f;
    [SerializeField]
    [Tooltip("How fast the player slows down if no input is applied")]
    private float friction = 1.0f;
    [SerializeField]
    [Tooltip("How fast the player accelerates")]
    private float acceleration = 1.0f;

    private Collider collider;
    private int extraJumpCounter;

    //Particles
    public ParticleSystem jumpSys;
    public ParticleSystem sprintSys;
    //Animation Properties
    public Animator anim;
    public GameObject visObject;

    //Camera
    public Camera characterCamera;
    Vector3 mousePosition;
    float deltaAccleration = 0;
    float fallTimer = 2.0f;

    public GameObject camPivot;

    public bool GroundPounding { get => groundPounding;}

    void Start()
    {
        extraJumpCounter = 1;
        playerSpeed = 5.0f;
        jumpForce = 5.0f;
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }
    private void CameraControls()
    {
        float minFOV = 60f;
        float maxFOV = 80f;
        float FOVmultiplier = 20;

        float yAcceleration = rb.velocity.y * Time.deltaTime;
        yAcceleration = Mathf.Abs(yAcceleration);
        deltaAccleration = yAcceleration - deltaAccleration;

        //Dynamically update FOV
        if (IsGrounded())
        {
            characterCamera.fieldOfView -= 0.05f;
        }
        if (deltaAccleration<=0 && fallTimer<=0)
        {
            characterCamera.fieldOfView -= 0.05f;
        }
        else
        {
            characterCamera.fieldOfView += yAcceleration * FOVmultiplier;
        }

        characterCamera.fieldOfView = Mathf.Clamp(characterCamera.fieldOfView, minFOV, maxFOV);


        //Temp
        if (Input.GetKey(KeyCode.E))
        {
            camPivot.transform.RotateAround(transform.position, Vector3.up, 90 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            camPivot.transform.RotateAround(transform.position, Vector3.up, -90 * Time.deltaTime);
        }
        /*if (Input.GetMouseButtonDown(0))
        {
            lastMousePositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            deltaMousePosition = Input.mousePosition.x - lastMousePositionX;
        }*/

    }
    private bool IsFalling()
    {
        if (rb.velocity.y < 0)
        {
            return true;
        }
        return false;
    }

    private bool IsGrounded()//checks if the player is touching the ground via raycasting
    {
        float distanceToGround = collider.bounds.extents.y;
        fallTimer = 2.0f;
        return Physics.Raycast(transform.position, -Vector3.up,distanceToGround +0.1f);
        ///for this to return true their must be a intercetion between the ray and the ground 
        ///because the box collider can prevent a intercection(distance can be zero and is therefore not interceting)
        ///so the +.1f ensures when the player lands their is a intercetion  
        
    }
    // Update is called once per frame

    public void InfluenceVelocity(Vector3 velAddition)
    {
        //Average velocity with argument velocity
        additionForce = velAddition;

        //rb.AddForce(velAddition, ForceMode.Acceleration);
    }
    void Update()
    {
        if (IsFalling())
        {
            fallTimer -= Time.deltaTime*900;
        }
        //print(fallTimer);
        CameraControls();
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
            StartCoroutine(JumpParticle()); //Play Particle effect
            extraJumpCounter--;
            rb.AddForce(new Vector3(0,jumpForce,0),ForceMode.Impulse);
        }

       
        //Disable player directional movement while in ice form
        if (PlayerState.currentPlayerState != PlayerMatterState.ICE)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && canSprint)
            {
                //Sprint
                if (PlayerState.currentPlayerState == PlayerMatterState.DROP)
                {
                    InfluenceVelocity(visObject.transform.forward * 250f);
                    StartCoroutine(SprintCooldown());
                }
            }

            //Add the force if not zero
            input += additionForce;

            //Change velocity
            if (input != Vector3.zero)
            {
                //Increase player speed gradually
                vel = Vector3.Lerp(vel, input, Time.deltaTime * acceleration);
            }
            else 
            {
                //If player is in cloud form, reduce velocity half as fast
                if (PlayerState.currentPlayerState == PlayerMatterState.CLOUD)
                {
                    vel = Vector3.Lerp(vel, input, Time.deltaTime * (friction / 2.0f));
                }
                else
                {
                    //Slow player down gradually
                    vel = Vector3.Lerp(vel, input, Time.deltaTime * friction);
                }
            }
            additionForce = Vector3.zero;
        }
        //PLAYER IS IN ICE FORM
        else
        {
            //Reduce speed over time
            vel = Vector3.Lerp( vel,Vector3.zero,Time.deltaTime * iceFriction);

            //Check for GroundPound

            if(!IsGrounded() && Input.GetKeyDown(KeyCode.LeftControl) && !groundPounding)
            {
                StartCoroutine(GroundPound());
            }

            if (additionForce != Vector3.zero)
            {
                vel += additionForce;
            }
            additionForce = Vector3.zero;
        }

        //Disable Gravity for cloud state
        if(PlayerState.currentPlayerState != PlayerMatterState.CLOUD)
        {
            rb.useGravity = true;
        }
        else
        {
            rb.useGravity = false;

            //Ensure player doesn't endlessly float upwards
            if(rb.velocity.y >= 0)
            {
                rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(rb.velocity.x, 0, rb.velocity.z), Time.deltaTime * cloudJumpDamper);
            }
        }

        //Update position

        //Set animator floats
        anim.SetFloat("HorizSpeed", input.magnitude);
        anim.SetFloat("VertSpeed", rb.velocity.y);

        //Make input relative to camera view
        Vector3 finalVel = Vector3.zero;

        finalVel += camPivot.transform.forward * vel.z;
        finalVel += camPivot.transform.right * vel.x;
        finalVel += camPivot.transform.up * vel.y;


        //If moving, update rotation angle
        if (input.magnitude != 0.0f)
        {
            float vertRot = Mathf.Atan2(finalVel.x, finalVel.z);

           visObject.transform.eulerAngles = new Vector3(visObject.transform.eulerAngles.x,
            Mathf.LerpAngle(visObject.transform.eulerAngles.y, Mathf.Rad2Deg * vertRot, Time.deltaTime * 15f)
            , visObject.transform.eulerAngles.z);
        }

        camPivot.transform.position = transform.position;
        transform.position += finalVel * Time.deltaTime * playerSpeed;

        //Check if grounded
        if (IsGrounded())
        {
            anim.SetBool("Grounded", true);
        }
        else if(Mathf.Abs(rb.velocity.y) > 0.0f)
        {
            anim.SetBool("Grounded", false);
        }
    }

    IEnumerator SprintCooldown()
    {
        canSprint = false;
        sprintSys.Play();
        yield return new WaitForSeconds(sprintCooldown);
        sprintSys.Stop();
        
        canSprint = true;
    }
    IEnumerator JumpParticle()
    {
        jumpSys.Play();

        yield return new WaitForSeconds(0.1f);
        jumpSys.Stop();
    }
    IEnumerator GroundPound()
    {
        rb.isKinematic = true;
        vel = Vector3.zero;

        //yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < 10; i++)
        {
            visObject.transform.Rotate(transform.right, 36.0f);
            yield return new WaitForSeconds(0.01f);
        }


        rb.isKinematic = false;
        InfluenceVelocity(new Vector3(0, -2f, 0));
        groundPounding = true;

        //Is falling
        while(!IsGrounded())
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.1f);

        groundPounding = false;
        //Prevent glitches through the grounds
        transform.position += Vector3.up * 0.1f;
    }
}
