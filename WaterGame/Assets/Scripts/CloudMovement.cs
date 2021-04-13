using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    float startYPos;
    public float magnitude;
    public float speed;

    float timeElapsed;
    // Update is called once per frame

    private void Start()
    {
        startYPos = transform.localPosition.y;
    }
    void Update()
    {

        //Move cloud model up and down (in local space) to give it "life"
        timeElapsed += Time.deltaTime * speed;
        transform.localPosition = new Vector3(0, startYPos + (Mathf.Sin(timeElapsed) * magnitude), 0);
    }
}
