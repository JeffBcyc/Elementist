using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] ElementType weakness;
    [SerializeField] float enemyHealth = 100;
    [SerializeField] ElementPickUp elementToSpawnAfterDeath;

    ElementBag elementBag;
    ElementBall damage;



    private void Start()
    {
        elementBag = FindObjectOfType<ElementBag>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MagicBall")
        {
            print("i'm hit by magic");
            ProcessDamage(other);
        }
    }


    private void ProcessDamage(Collider other)
    {
        damage = other.GetComponent<ElementBall>();

        if (damage.CombinedMagicType == weakness)
        {
            print("Enemey taking damage: " + damage.CombinedMagicType + " - " + damage.CombinedMagicDamage);
            enemyHealth -= damage.CombinedMagicDamage;
        } else
        {
            print("Enemey taking damage: " + damage.CombinedMagicType + " - " + damage.CombinedMagicDamage/2);
            enemyHealth -= damage.CombinedMagicDamage/2;
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


}
