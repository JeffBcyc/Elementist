﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] float enemyHealth = 2;
    [SerializeField] ElementPickUp[] elementToSpawnAfterDeath;
    [SerializeField] EnemyStatus enemyStatus;
    [SerializeField] HealthBar healthBar;
    [SerializeField] ElementType dropElement;
    [SerializeField] EnemyCounter enemyCounter;
    [SerializeField] TextMesh textMesh;

    public float CurrentHealth
    {
        get { return enemyHealth; }
    }

    private enum EnemyStatus
    {
        Wet,
        Burning,
        Electrified,
        Thinking
    }

    ElementBag elementBag;
    ElementBall damage;


    private void Start()
    {
        enemyCounter = FindObjectOfType<EnemyCounter>();
        enemyStatus = EnemyStatus.Thinking;
        elementBag = FindObjectOfType<ElementBag>();
        healthBar.SetMaxHealth(enemyHealth);
        textMesh.text =  enemyStatus.ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MagicBall")
        {
            ProcessDamage(other);
        }
    }


    private void ProcessDamage(Collider other)
    {
        damage = other.GetComponent<ElementBall>();
        float _modifiedDamage = ModifyDamageByStatus(damage.CombinedMagicDamage);
        enemyHealth -= _modifiedDamage;
        healthBar.SetHealth(enemyHealth);

        if (enemyHealth <= 0)
        {
            SpawnElement();
            enemyCounter.EnemyCountDecrease();
            Destroy(gameObject, 5f);
        }
        else
        {
            enemyStatus = ChangeEnemyStatus(damage.CombinedMagicType);
            textMesh.text = enemyStatus.ToString();
        }
    }

    private float ModifyDamageByStatus(float rawDamage)
    {
        float modifiedDamage = rawDamage;

        if (enemyStatus == EnemyStatus.Electrified)
        {

            switch (damage.CombinedMagicType)
            {
                case ElementType.Ice:
                    modifiedDamage = damage.CombinedMagicDamage * 2;
                    break;
                case ElementType.Fire:
                    modifiedDamage = damage.CombinedMagicDamage * 2;
                    break;
            }
        }
        else if (enemyStatus == EnemyStatus.Burning)
        {
            switch (damage.CombinedMagicType)
            {
                case ElementType.Electric:
                    modifiedDamage = damage.CombinedMagicDamage / 2;
                    break;
                case ElementType.Ice:
                    modifiedDamage = damage.CombinedMagicDamage / 2;
                    break;
            }
        }
        else if (enemyStatus == EnemyStatus.Wet)
        {
            switch (damage.CombinedMagicType)
            {
                case ElementType.Fire:
                    modifiedDamage = damage.CombinedMagicDamage / 2;
                    break;
                case ElementType.Electric:
                    modifiedDamage = damage.CombinedMagicDamage * 2;
                    break;
            }
        }

        return modifiedDamage;
    }

    private EnemyStatus ChangeEnemyStatus(ElementType combinedMagicType)
    {
        EnemyStatus _newstatus;
        if (combinedMagicType == ElementType.Electric) _newstatus = EnemyStatus.Electrified;
        else if (combinedMagicType == ElementType.Fire) _newstatus = EnemyStatus.Burning;
        else if (combinedMagicType == ElementType.Ice) _newstatus = EnemyStatus.Wet;
        else _newstatus = EnemyStatus.Thinking;
        return _newstatus;
    }

    private void SpawnElement()
    {
        foreach (ElementPickUp item in elementToSpawnAfterDeath)
        {
            if (item.ElementFromThisBook == dropElement)
            {
                Instantiate(item, transform.position, Quaternion.identity);
                break;
            }
        }
    }


}
