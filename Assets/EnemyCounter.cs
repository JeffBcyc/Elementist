using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{

    [SerializeField] SceneController sceneController;
    [SerializeField] int enemyCount;
    private void Awake()
    {
        enemyCount = FindObjectsOfType<EnemyHealth>().Length;
        sceneController = FindObjectOfType<SceneController>();
    }

    private void Update()
    {
        if (enemyCount == 0)
        {
            StartCoroutine(LoadNext());
            
        }
    }

    IEnumerator LoadNext()
    {
        yield return new WaitForSeconds(5f);
        sceneController.LoadNextScene();
    }

    public void EnemyCountDecrease()
    {
        enemyCount--;
    }
}
