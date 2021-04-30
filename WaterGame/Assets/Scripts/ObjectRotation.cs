using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    float startYPos;

    [Header("Object Bob")]
    public float magnitude;
    public float bobSpeed;

    float timeElapsed;

    [Header("Object Rotation")]

    public Vector3 axis;
    public float rotSpeed;
    // Update is called once per frame

    private void Start()
    {
        startYPos = transform.localPosition.y;
    }
    void Update()
    {
        //Rotation
        transform.Rotate(axis.normalized, rotSpeed * Time.deltaTime,Space.World);

        //Move model up and down (in local space)
        timeElapsed += Time.deltaTime * bobSpeed;
        transform.localPosition = new Vector3(0, startYPos + (Mathf.Sin(timeElapsed) * magnitude), 0);
    }
}
