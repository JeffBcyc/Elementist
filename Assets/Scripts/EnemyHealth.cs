using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] ElementType resistance;
    [SerializeField] float enemyHealth = 100;
    [SerializeField] ElementPickUp elementToSpawnAfterDeath;

    ElementBag magicCombo;
    Dictionary<ElementType, float> currentDamageBook;

    float damageReceived;

    private void Start()
    {
        magicCombo = FindObjectOfType<ElementBag>();
        currentDamageBook = magicCombo.GetDamageBook();
    }

    private void Update()
    {
        //foreach (var entry in currentDamageBook)
        //{
        //    print(entry.Key + " : " + entry.Value);
        //}
        print(currentDamageBook.First().Key);
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        ProcessDamage();
    }


    private void ProcessDamage()
    {
        currentDamageBook = magicCombo.GetDamageBook();

        foreach (var entry in currentDamageBook)
        {
            if (entry.Key == resistance)
            {
                float modifiedDamage = entry.Value / 2;
                print("Enemey taking " + modifiedDamage + " Damage, of type " + entry.Key);
                enemyHealth -= modifiedDamage;
            } else
            {
                float modifiedDamage = entry.Value;
                print("Enemey taking " + modifiedDamage + " Damage, of type " + entry.Key);
                enemyHealth -= modifiedDamage;
            }
        }

        //CheckForResistance();
        //damageReceived = currentDamageBook.Sum(x => x.Value);
        //enemyHealth -= damageReceived;
        if (enemyHealth <= 0)
        {
            SpawnElement();
            Destroy(gameObject);
            print("Enemy Exploded!");
        }
    }

    private void SpawnElement()
    {
        Instantiate(elementToSpawnAfterDeath, transform.position, Quaternion.identity);
    }

    private void CheckForResistance()
    {
        if (resistance.Equals(ElementType.Empty)) return;
        try
        {
            currentDamageBook[resistance] /= 2;
        }
        catch
        {
            //print("no fire element in the bag");
        }
    }
}
