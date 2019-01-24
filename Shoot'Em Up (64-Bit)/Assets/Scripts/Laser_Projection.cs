using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Projection : MonoBehaviour
{
    public float laser_velocity;
    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * laser_velocity;
    }
}
