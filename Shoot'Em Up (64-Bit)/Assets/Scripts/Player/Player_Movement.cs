using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    public GameObject DeathAreaPrefab;

    public static Vector3 originPosition;

    //Initializing speed and the speed of rotation
    [HideInInspector] public float speed, rotationSpeed; //Current Speed and Rotation Speed
    public float maxSpeed, maxRotationSpeed; //The Max Speed and the Max Speed of Rotation
    public float rateOfSpeed, rateOfRotation; //The amount of speed and rotation speed you gain over time.

    public bool enableRevertControl = true;
    
    private float rotation; //Rotational controls: It will return the value of the controller or keyboard (-1, 0, 1)

    private bool ThrottleDown, reverseThrottleDown;

    private Rigidbody2D rb; //Giving an identifier (or name) that'll reference our RigidBody!!

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //Grab the component of the local RigidBody
        originPosition = gameObject.transform.position;
    }

    void Update()
    {
        rotation = Input.GetAxisRaw("Horizontal"); //The left, right, A, or D key
    }

    void FixedUpdate()
    {
        switch (enableRevertControl) {
            case false:

                Rotate(-rotation); //The variable r is used as a parameter for the Rotate function

                if (Input.GetKey(KeyCode.W))
                    Throttle();
                else if (Input.GetKey(KeyCode.S))
                    ReverseThrottle();

                break;

            case true:

                Rotate(rotation);

                if (Input.GetKey(KeyCode.S))
                    ReverseThrottle();
                else if (Input.GetKey(KeyCode.W))
                    Throttle();

                break;

        }
    }

    float Rotate(float dir)
    {
        

        if (Mathf.Abs(rotationSpeed) < Mathf.Abs(maxRotationSpeed))
        {
            rotationSpeed += rateOfRotation; //The rotation speed is not assigned a number (except perhaps 0).
                                             //It will increment by the rateOfRotation times dir (which has values of 1, -1)
        }


        transform.Rotate(dir * Vector3.forward, rotationSpeed); //We then update the rotation of the GameObject every frame, the rotationSpeed being how fast the rotation is.

        return dir; //Retunring the value dir for the Update function.
    } //Controls the rotation of the player

    void Throttle()
    {
        switch (enableRevertControl)
        {
            case false:
                ThrottleDown = Input.GetKey(KeyCode.W);
                break;
            case true:
                ThrottleDown = Input.GetKey(KeyCode.S);
                break;
        }

        //Going fowards
        if (ThrottleDown == true)
        {

            if (Mathf.Abs(speed) * -1 > Mathf.Abs(maxSpeed) * -1)
                speed += rateOfSpeed;


            rb.AddForce(speed * (transform.localRotation * new Vector2(rotationSpeed, 0f)));

        }
        else
        {
            if (Mathf.Abs(speed) * -1 < 0)
                speed -= rateOfSpeed * Mathf.Sign(speed);
        }
    } //Propels the player foward

    void ReverseThrottle()
    {
        switch(enableRevertControl)
        {
            case false:
                reverseThrottleDown = Input.GetKey(KeyCode.S);
                break;
            case true:
                reverseThrottleDown = Input.GetKey(KeyCode.W);
                break;
        }

        //Going backwards
        if (reverseThrottleDown == true) {

            if (Mathf.Abs(speed) < Mathf.Abs(maxSpeed))
                speed += rateOfSpeed;


            rb.AddForce(speed * (transform.localRotation * new Vector2(-rotationSpeed, 0f)));

        } else
        {
            if (Mathf.Abs(speed) > 0)
                speed -= rateOfSpeed * Mathf.Sign(-speed);
        }
    } //Propels the player to the opposite direction

    void OnTriggerEnter2D(Collider2D varObject)
    {
        if (varObject.tag == "Enemy" || varObject.tag == "Asteroid")
        {
            if (gameManager.instance.lives > 0)
            {
                gameManager.instance.lives--;
                gameObject.transform.position = originPosition;
                gameManager.instance.RemoveEnemies();
                Debug.Log("You now have a total of " + gameManager.instance.lives + " lives.");
            }
            else
            {
                Debug.LogWarning("Player Ship got Destoryed.");
                Instantiate(DeathAreaPrefab);
                DeathAreaPrefab.transform.position = gameObject.transform.position;
                Destroy(gameObject);
            }
            gameObject.transform.position = originPosition;
        }
    }
}
