using UnityEngine;
using System.Collections;

public class Collision: MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
         Destroy(gameObject);
         Debug.Log("Player ship got destory by being out of bounds!!!");
    }
}
