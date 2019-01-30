using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{

    private void Update()
    {
        KeyCommand();
    }

    void KeyCommand()
    {
        //Simply quits the game when on standalone
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();

        //Restarts the scene (or restarts the game)
        if (Input.GetKey(KeyCode.F1))
            SceneManager.LoadScene("Playing Field", 0);
    }

    void InstanceTracker()
    {
        GameObject[] instance = GameObject.FindGameObjectsWithTag("Enemy000");
        int instanceCount = instance.Length;
    }
}
