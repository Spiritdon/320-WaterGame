using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    float visObjectLerpSpeed = 30f;


    Vector3 relativeVel = Vector3.zero;


    //Dash stuff
    [SerializeField] float dashCooldown = 1.0f;
    bool canDash = true;
    bool isDashing = false;
    float dashTime = 0.01f;
    float dashTimeElapsed = 0.0f;

    float cameraRotTarget;

    //Is the player Ground pounding
    bool groundPounding = false;
 
    [Header("Player movement attributes")]
    [SerializeField]
    private float iceFriction = 0f;
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
    public GameObject cloudVis;
    private Vector3 cloudBaseScale;

    //Camera
    public GameObject camPivot;


    public bool GroundPounding { get => groundPounding;}

    //Pause Menu
    public GameObject panel;
    bool isPanelActive;

    public LayerMask trampCheckLayer;

    private float floatTimer;
    private float floatForce;
    private float cloudGravity;

    void Start()
    {
        cloudGravity = Physics.gravity.y / 20;
        floatForce = 0.5f;
        floatTimer = 3;
        extraJumpCounter = 1;
        playerSpeed = 5.0f;
        jumpForce = 5.0f;
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cloudBaseScale = cloudVis.transform.localScale;
    }

    public bool IsFalling()
    {
        if (rb.velocity.y < -0.1f)
        {
            return true;
        }
        return false;
    }

    public bool IsGrounded()//checks if the player is touching the ground via raycasting
    {
        float distanceToGround = collider.bounds.extents.y;
        return Physics.Raycast(transform.position, -Vector3.up,distanceToGround +0.1f);
        ///for this to return true their must be a intercetion between the ray and the ground 
        ///because the box collider can prevent a intercection(distance can be zero and is therefore not interceting)
        ///so the +.1f ensures when the player lands their is a intercetion  
        
    }
    public bool IsTrampHit()
    {
        if (PlayerState.currentPlayerState == PlayerMatterState.ICE)
        {
            float distanceToGround = collider.bounds.extents.y;
            return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.2f, trampCheckLayer);
        }
        return false;
    }
    // Update is called once per frame

    public void InfluenceVelocity(Vector3 velAddition)
    {
        //Average velocity with argument velocity
        additionForce += velAddition;

        //rb.AddForce(velAddition, ForceMode.Acceleration);
    }
    public void ClosePauseMenu()
    {
        panel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
    }
    void Update()
    {
        //Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPanelActive = panel.activeSelf;
            isPanelActive = !isPanelActive;
            panel.SetActive(isPanelActive);
            if (isPanelActive)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1.0f;
            }
        }
       
        //print(fallTimer);
        
        //basic player movement
        movementX = Input.GetAxis("Horizontal");
        movementZ = Input.GetAxis("Vertical");

        
        Vector3 input = new Vector3(movementX, 0.0f, movementZ);

        //code that determines if the player has double jumped
        if (IsGrounded())//if the player is touching the ground replenish his double jump
        {
            extraJumpCounter = 1;
            floatTimer = 3;
        }
        if (PlayerState.currentPlayerState == PlayerMatterState.ICE || PlayerState.currentPlayerState == PlayerMatterState.DROP)
        {
            if (Input.GetKeyDown(KeyCode.Space) && extraJumpCounter != 0)//allow the player to jump as long as they are pressing space and they have extra jumps
            {
                StartCoroutine(JumpParticle()); //Play Particle effect
                extraJumpCounter--;
                float momentumY = Mathf.Abs(rb.mass * rb.velocity.y);
                //this calculates the momentum (Momentum = Max * Velocity) 
                //The Force of Momentum is (Force of Momentum = Momentum/Deltatime) 
                //Force is instantous so time isnt a factor so Force of Momentum
                //So Force Of Momentum = Momentum;
                if (IsFalling())//canceling out the Force of Momentum for Video Game Double Jumping When Falling
                {
                    float tempForce = jumpForce + momentumY;
                    rb.AddForce(new Vector3(0, tempForce, 0), ForceMode.Impulse);
                }
                else//Other Wise Jump Normally
                {
                    rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                }
            }
        }
        else
        {
            //Cloud Float?
            if (Input.GetKey(KeyCode.Space) && floatTimer > 0)//allow the player to jump as long as they are pressing space and they have extra jumps
            {
                rb.AddForce(new Vector3(0, floatForce, 0), ForceMode.Acceleration);
                floatTimer -= Time.deltaTime;
                cloudVis.transform.localScale = Vector3.Lerp(cloudVis.transform.localScale, cloudBaseScale * 0.3f, Time.deltaTime / 3.0f);
            }
            else
            {
                cloudVis.transform.localScale = Vector3.Lerp(cloudVis.transform.localScale, cloudBaseScale, Time.deltaTime * 10f);
            }
        }


        if(isDashing)
        {
            dashTimeElapsed += Time.deltaTime;

            if (dashTimeElapsed <= dashTime)
            {
                InfluenceVelocity(visObject.transform.forward * 100f);
            }
            else
            {
                isDashing = false;
                dashTimeElapsed = 0.0f;
                StartCoroutine(SprintCooldown());
            }
        }
       
        //Disable player directional movement while in ice form
        if (PlayerState.currentPlayerState != PlayerMatterState.ICE)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                //Dash
                if (PlayerState.currentPlayerState == PlayerMatterState.DROP)
                {
                    isDashing = true;
                }
            }

            ////Add the force if not zero
            //input += additionForce;

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
            //additionForce = Vector3.zero;
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
            
        }

        //Disable Gravity for cloud state
        if(PlayerState.currentPlayerState != PlayerMatterState.CLOUD)
        {
            rb.useGravity = true;
        }
        else
        {
            
            rb.useGravity = false;
            rb.AddForce(new Vector3(0, cloudGravity, 0), ForceMode.Acceleration);
            //Ensure player doesn't endlessly float upwards
            if (rb.velocity.y >= 0)
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


        //TODO:Ethan - Clean this up
        finalVel += additionForce;

        relativeVel = Vector3.Lerp(relativeVel, finalVel, Time.deltaTime * acceleration);

        //If moving, update rotation angle
        if (input.magnitude != 0.0f)
        {
            float vertRot = Mathf.Atan2(finalVel.x, finalVel.z);

           visObject.transform.eulerAngles = new Vector3(visObject.transform.eulerAngles.x,
            Mathf.LerpAngle(visObject.transform.eulerAngles.y, Mathf.Rad2Deg * vertRot, Time.deltaTime * 15f)
            , visObject.transform.eulerAngles.z);
        }


        transform.position += relativeVel * Time.deltaTime * playerSpeed;
        visObject.transform.position = Vector3.Lerp(visObject.transform.position, transform.position, visObjectLerpSpeed * Time.deltaTime);

        //Check if grounded
        if (IsGrounded())
        {
            anim.SetBool("Grounded", true);
        }
        else if(Mathf.Abs(rb.velocity.y) > 0.0f)
        {
            anim.SetBool("Grounded", false);
        }


        /* Auto Camera Rotation
        
        if (input.x != 0.0f)
        {
            cameraRotTarget = visObject.transform.eulerAngles.y;
        }

        camPivot.transform.eulerAngles =
                 new Vector3(camPivot.transform.eulerAngles.x, Mathf.LerpAngle(camPivot.transform.eulerAngles.y, cameraRotTarget, Time.deltaTime * 2.5f), camPivot.transform.eulerAngles.z);

         */
        //Reset addition force
        additionForce = Vector3.zero;
    }

    IEnumerator SprintCooldown()
    {
        canDash = false;
        sprintSys.Play();
        yield return new WaitForSeconds(dashCooldown);
        sprintSys.Stop();
        
        canDash = true;
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
