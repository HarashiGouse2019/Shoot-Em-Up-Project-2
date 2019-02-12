using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Random = UnityEngine.Random;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public GameObject[] spawnPoint;

    public float astroidVelocity;

    public List<GameObject> enemies;

    public float enemySpeed;
    public float enemyShipRotation;

    public List<GameObject> activeEnemies;
    public bool removingEnemies;
    public int maximumNumberOfActiveObstacles;

    public GameObject player, DeathAreaPrefab;

    public int lives;

    IEnumerator coroutine;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        activeEnemies = new List<GameObject>();
        removingEnemies = false;
    }

    void Update()
    {
         KeyCommand();
         for (int i = 0; i < maximumNumberOfActiveObstacles; i++)
            AddEnemy();

        coroutine = WaitForGameEnd(6);

         if (lives < 1)
            StartCoroutine(coroutine);
        
    }

    void KeyCommand()
    {
        //Simply quits the game when on standalone
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    void AddEnemy()
    {

        if (activeEnemies.Count < maximumNumberOfActiveObstacles)
        {
            // Determine spawn point
            int id = Random.Range(0, spawnPoint.Length);
            GameObject point = spawnPoint[id];

            // Determine which enemy to spawn
            GameObject enemy = enemies[Random.Range(0, enemies.Count)];

            // Instantiate an enemy
            GameObject enemyInstance = Instantiate<GameObject>(enemy, point.transform.position, Quaternion.identity);

            if (enemyInstance.GetComponent<Asteroid>() != null)
            {
                Vector2 directionVector = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                directionVector.Normalize();
                enemyInstance.GetComponent<Asteroid>().direction = directionVector;
            }

            // Add to enemies list
            activeEnemies.Add(enemyInstance);
        }

    }

    public void RemoveEnemies()
    {
        removingEnemies = true;
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            Destroy(activeEnemies[i]);
        }
        activeEnemies.Clear();
        removingEnemies = false;
    }

    IEnumerator WaitForGameEnd(int time)
    {
        yield return new WaitForSeconds(time);
        Application.Quit();
        Debug.Log("GAME OVER DESU!!!");
    }
}