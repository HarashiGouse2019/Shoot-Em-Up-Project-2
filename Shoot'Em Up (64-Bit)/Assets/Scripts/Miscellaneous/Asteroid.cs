using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public Vector2 direction;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * gameManager.instance.astroidVelocity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //If the tag of an object is the bullet
        if (other.gameObject.tag == "Bullet")
        {
            gameManager.instance.activeEnemies.Remove(this.gameObject);
            Destroy(this.gameObject);

            // Also destroy bullet
            Destroy(other.gameObject);
        }

        //If the tag of an object is the player
        if (other.gameObject.tag == "Player")
        {
            gameManager.instance.activeEnemies.Remove(this.gameObject);
            Destroy(this.gameObject);
        }

        //If it is outside of the playing field
        if (other.gameObject.tag == "Board")
        {
            Debug.Log("Asteroid in Playing Field");
        }
        Debug.Log(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!gameManager.instance.removingEnemies)
        {
            if (other.gameObject.tag == "Board")
            {
                gameManager.instance.activeEnemies.Remove(this.gameObject);
                Debug.Log("Astroid got destoryed!!!");
                Destroy(this.gameObject);
            }
        }
    }
}
