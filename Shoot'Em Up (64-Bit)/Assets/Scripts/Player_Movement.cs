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
    public bool enableInstantVelocity = false;

    private float clockwise; //Reads values 1 and -1; -1 for counter-clockwise, 1 for clockwise rotation
    private float r; //Rotational controls: It will return the value of the controller or keyboard (-1, 0, 1)

    private bool instantVelocityReady;

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

        Debug.Log("Current Speed: " + speed);
        Debug.Log("Current Rotation Speed: " + rotationSpeed);

        string dirString = null; //This is a string that simply tells us "Yes" or "No" to rather we are going clockwise or not.
        clockwise = Input.GetAxis("Horizontal"); //The clockwise variable will be assingned the sign of the rotation speed (1, 0, -1)

        if (Mathf.Sign(clockwise) == 1)
            dirString = "NO";
        else if (Mathf.Sign(clockwise) == -1)
            dirString = "YES";

        Debug.Log("Clockwise Value?: " + clockwise + " (" + dirString + ")"); //This will log a message to the console for the designer to know rather he's going clockwise or counterclockwise.
    }

    void FixedUpdate()
    {
        Start_Piloting();
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
    }

    void Start_Piloting()
    {
        bool throttleDown = Input.GetKey(KeyCode.W);

        if (throttleDown == true) {
            instantVelocityReady = false;

            if (Mathf.Abs(speed) < Mathf.Abs(maxSpeed))
                speed += rateOfSpeed;

        
            rb.AddForce(speed * (transform.localRotation * new Vector2(rotationSpeed, 0f)));
            
        }
        else
        {
            instantVelocityReady = true;
            if (Mathf.Abs(speed) > 0)
                speed -= (rateOfSpeed * 2) * Mathf.Sign(speed);
            if (Input.GetKey(KeyCode.UpArrow))
                Launch();
        }

    }

    void Launch()
    {
        if (instantVelocityReady)
        {
            speed = maxSpeed;
            rb.velocity = speed * (transform.localRotation * new Vector2(rotationSpeed, 0f));
            setTimer(1000);
        }
    }

    int setTimer(int unit)
    {
        if (unit < 1000)
            Debug.LogError("setTimer parameter must be greater than 1000");
        else
        {
            instantVelocityReady = false;
            int timePos = unit;
            do
            {
                timePos--;
            } while (timePos > 0);
        }
        return unit;
    }
}
