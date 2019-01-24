using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    private void Update()
    {
        //Simply quits the game when on standalone
        if (Input.GetKey(KeyCode.Space))
            Application.Quit();

        //Restarts the scene (or restarts the game)
        if (Input.GetKey(KeyCode.F1))
            SceneManager.LoadScene("Playing Field", 0);
    }

}
