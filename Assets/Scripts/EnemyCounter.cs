using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{


    [SerializeField] SceneController sceneController;
    [SerializeField] int enemyCount;


    public float FadeSpeed = 10.0F;
    public int RolloverCharacterSpread = 10;
    public Color ColorTint;

    private void Awake()
    {
        enemyCount = FindObjectsOfType<EnemyHealth>().Length;
        sceneController = FindObjectOfType<SceneController>();
    }

    private void Update()
    {
        if (enemyCount == 0)
        {
            // show victory screen 
        }
    }

    public void EnemyCountDecrease()
    {
        enemyCount--;
    }


    


}
