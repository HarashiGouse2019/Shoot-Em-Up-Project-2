using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Commands : MonoBehaviour
{
    private void Update()
    {

        if (Input.GetKey(KeyCode.Space))
            Application.Quit();
        if (Input.GetKey(KeyCode.F1))
            SceneManager.LoadScene("Playing Field", 0);
    }

}
