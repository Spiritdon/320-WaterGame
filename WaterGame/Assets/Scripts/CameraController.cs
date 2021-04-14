using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //References
    [SerializeField] Rigidbody playerRb;
    PlayerMovement playerObject;
    public Camera characterCamera;
    Vector3 mousePosition;

    //Fields
    float deltaAccleration = 0;
    float fallTimer = 2.0f;
    [SerializeField] float movementSpeed = 100f;    //Higher values = more responsive



    // Start is called before the first frame update
    void Start()
    {
        playerObject = playerRb.gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        float minFOV = 60f;
        float maxFOV = 80f;
        float FOVmultiplier = 20;

        float yAcceleration = playerRb.velocity.y * Time.deltaTime;
        yAcceleration = Mathf.Abs(yAcceleration);
        deltaAccleration = yAcceleration - deltaAccleration;

        //Dynamically update FOV
        if (playerObject.IsGrounded())
        {
            fallTimer = 2.0f;
            characterCamera.fieldOfView -= 0.05f;
        }
        if (deltaAccleration <= 0 && fallTimer <= 0)
        {
            characterCamera.fieldOfView -= 0.05f;
        }
        else
        {
            characterCamera.fieldOfView += yAcceleration * FOVmultiplier;
        }

        //Set camera fov
        characterCamera.fieldOfView = Mathf.Clamp(characterCamera.fieldOfView, minFOV, maxFOV);


        if (playerObject.IsFalling())
        {
            fallTimer -= Time.deltaTime * 900;
        }

        //Temp
        if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(transform.position, Vector3.up, 90 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(transform.position, Vector3.up, -90 * Time.deltaTime);
        }
        /*if (Input.GetMouseButtonDown(0))
        {
            lastMousePositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            deltaMousePosition = Input.mousePosition.x - lastMousePositionX;
        }*/

        //Match player position
        transform.position = Vector3.Lerp(transform.position, playerObject.transform.position, Time.deltaTime * movementSpeed);
       
    }
}
