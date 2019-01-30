using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearBullet : MonoBehaviour
{

    private int time = 0;
    private IEnumerator coroutine;

    void Awake()
    {
        coroutine = TimeDestroyObject();
    }

    void Update()
    {
        if (gameObject.activeInHierarchy) {
            time++;
            Debug.Log("Seconds passed: " + time);
            if (time > 60)
                Destroy(gameObject);
            StartCoroutine(coroutine);
        }
    }

    IEnumerator TimeDestroyObject()
    {
        yield return new WaitForSeconds(1);
    }
}
