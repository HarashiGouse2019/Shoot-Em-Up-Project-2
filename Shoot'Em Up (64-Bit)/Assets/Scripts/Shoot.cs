using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform pointOfFire; //We assign the gameObject that is the child to our Player GameObject
    public GameObject bulletPrefab; //We assign a Prefab to the slot in order to spawn it when we shoot

    [Range(1f,10f)] public float recoilTime;

    public bool enableAutomaticMode;

    private bool isKeyReleased;
    private IEnumerator coroutine;

    void Update()
    {
        if (Input.GetKey(KeyCode.J) || isKeyReleased == true)
        {
            coroutine = Recoil();
            switch (enableAutomaticMode) {
                case false:
                    isKeyReleased = false;
                    Instantiate(bulletPrefab, pointOfFire.position, pointOfFire.rotation); //A bullet will spawn with a set direction based on the player's direction
                    FindObjectOfType<Audio_Manager>().Play("Shoot");
                    break;

                case true:
                    isKeyReleased = false;
                    Instantiate(bulletPrefab, pointOfFire.position, pointOfFire.rotation); //A bullet will spawn with a set direction based on the player's direction
                    FindObjectOfType<Audio_Manager>().Play("Shoot");
                    StartCoroutine(coroutine);
                    break;
            }
        }
    }
   

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //Collision Code Goes Here!!! xD
    }

    private IEnumerator Recoil()
    {
        yield return new WaitForSeconds(recoilTime);
        isKeyReleased = true;
    }

}
