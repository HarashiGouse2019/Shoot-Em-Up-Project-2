using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform pointOfFire;
    public GameObject bulletPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) 
            Instantiate(bulletPrefab, pointOfFire.position, pointOfFire.rotation);
    }

}
