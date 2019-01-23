using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    //Initializing speed and the speed of rotation
    [HideInInspector] public float speed, rotationSpeed; //Current Speed and Rotation Speed
    public float maxSpeed, maxRotationSpeed; //The Max Speed and the Max Speed of Rotation
    public float rateOfSpeed, rateOfRotation; //The amount of speed and rotation speed you gain over time.

    private float clockwise; //Reads values 1 and -1; -1 for counter-clockwise, 1 for clockwise rotation
    private float r; //Rotational controls: It will return the value of the controller or keyboard (-1, 0, 1)
    private Rigidbody2D rb; //Giving an identifier (or name) that'll reference our RigidBody!!

    void Awake()
    {
        Vector3 pVector = new Vector3(0.4f, 0.0f, 0.0f);
        rb = GetComponent<Rigidbody2D>(); //Grab the component of the local RigidBody
    }

    void Update()
    {
        r = Input.GetAxisRaw("Horizontal"); //The left, right, A, or D keys
        Rotate(r); //The variable r is used as a parameter for the Rotate function

        if (Input.GetKey(KeyCode.UpArrow))
            Propel();
    }

    float Rotate(float dir)
    {
        string dirString = null; //This is a string that simply tells us "Yes" or "No" to rather we are going clockwise or not.
        clockwise = Mathf.Sign(rotationSpeed); //The clockwise variable will be assingned the sign of the rotation speed (1, 0, -1)

        if (clockwise == -1)
            dirString = "NO";
        else if (clockwise == 1)
            dirString = "YES";

        Debug.Log("Clockwise Value?: " + clockwise + " (" + dirString + ")"); //This will log a message to the console for the designer to know rather he's going clockwise or counterclockwise.

        if (Mathf.Abs(rotationSpeed) < Mathf.Abs(maxRotationSpeed))
            rotationSpeed += rateOfRotation * dir; //The rotation speed is not assigned a number (except perhaps 0).
                                                   //It will increment by the rateOfRotation times dir (which has values of 1, -1)

        transform.Rotate(dir * (Vector3.forward * Time.deltaTime), rotationSpeed); //We then update the rotation of the GameObject every frame, the rotationSpeed being how fast the rotation is.

        return dir; //Retunring the value dir for the Update function.
    }

    void Propel()
    {
        if (Mathf.Abs(speed) < Mathf.Abs(maxSpeed))
        {
            rb.velocity = pVector * speed;
        }
        
    }
}
