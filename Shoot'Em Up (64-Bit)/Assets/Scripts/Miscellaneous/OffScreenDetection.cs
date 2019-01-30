using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class OffScreenDetection : MonoBehaviour
{

    public Transform targetPoint;
    public GameObject Player;

    private Camera camera;

    void Awake()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameObject.Find("SpaceShip") != null) 
        {
            Vector3 screenPoint = camera.WorldToViewportPoint(targetPoint.position);
            bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

            if (!onScreen)
            {
                Debug.LogWarning("Player Ship is out of bounds!!!!");
                Destroy(Player);
            }
        }
    }
}