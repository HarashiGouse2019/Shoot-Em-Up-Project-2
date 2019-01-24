using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform pointOfFire; //We assign the gameObject that is the child to our Player GameObject
    public GameObject bulletPrefab; //We assign a Prefab to the slot in order to spawn it when we shoot

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) 
            Instantiate(bulletPrefab, pointOfFire.position, pointOfFire.rotation); //A bullet will spawn with a set direction based on the player's direction
    }

}
