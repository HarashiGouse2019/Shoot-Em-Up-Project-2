using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    Vector3 defaultPosition;

    //Initializing speed and the speed of rotation
    [HideInInspector] public float speed, rotationSpeed; //Current Speed and Rotation Speed
    public float maxSpeed, maxRotationSpeed; //The Max Speed and the Max Speed of Rotation
    public float rateOfSpeed, rateOfRotation; //The amount of speed and rotation speed you gain over time.

    public bool enableRevertControl = true;

    private float clockwise; //Reads values 1 and -1; -1 for counter-clockwise, 1 for clockwise rotation
    private float r; //Rotational controls: It will return the value of the controller or keyboard (-1, 0, 1)

    private Rigidbody2D rb; //Giving an identifier (or name) that'll reference our RigidBody!!

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //Grab the component of the local RigidBody
    }

    void Update()
    {
        r = Input.GetAxisRaw("Horizontal"); //The left, right, A, or D keys

        switch (enableRevertControl)
        {
            case false:
                Rotate(-r); //The variable r is used as a parameter for the Rotate function
                break;
            case true:
                Rotate(r);
                break;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W)) Throttle();
        if (Input.GetKey(KeyCode.S)) ReverseThrottle();
    }

    float Rotate(float dir)
    {
        

        if (Mathf.Abs(rotationSpeed) < Mathf.Abs(maxRotationSpeed) || Input.GetKey(KeyCode.UpArrow))
        {
            rotationSpeed += rateOfRotation; //The rotation speed is not assigned a number (except perhaps 0).
                                             //It will increment by the rateOfRotation times dir (which has values of 1, -1)
        }


        transform.Rotate(dir * Vector3.forward, rotationSpeed); //We then update the rotation of the GameObject every frame, the rotationSpeed being how fast the rotation is.

        return dir; //Retunring the value dir for the Update function.
    } //Controls the rotation of the player

    void Throttle()
    {

        bool ThrottleDown = Input.GetKey(KeyCode.W);

        //Going backwards
        if (ThrottleDown == true)
        {

            if (Mathf.Abs(speed) * -1 > Mathf.Abs(maxSpeed) * -1)
                speed -= rateOfSpeed;


            rb.AddForce(speed * (transform.localRotation * new Vector2(-rotationSpeed, 0f)));

        }
        else
        {
            if (Mathf.Abs(speed) * -1 < 0)
                speed += rateOfSpeed * Mathf.Sign(speed);
        }
    } //Propels the player foward

    void ReverseThrottle()
    {
        bool reverseThrottleDown = Input.GetKey(KeyCode.S);

        //Going foward
        if (reverseThrottleDown == true) {

            if (Mathf.Abs(speed) < Mathf.Abs(maxSpeed))
                speed += rateOfSpeed;


            rb.AddForce(speed * (transform.localRotation * new Vector2(rotationSpeed, 0f)));

        } else
        {
            if (Mathf.Abs(speed) > 0)
                speed -= rateOfSpeed * Mathf.Sign(speed);
        }
    } //Propels the player to the opposite direction

}
