using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] MagicType resistance;
    [SerializeField] float enemyHealth = 100;

    MagicCombo magicCombo;
    Dictionary<MagicType, float> currentDamageBook;

    float damageReceived;

    private void Start()
    {
        magicCombo = FindObjectOfType<MagicCombo>();
    }

    private void Update()
    {
        OnDamageTaken();
    }


    private void OnDamageTaken()
    {
        currentDamageBook = magicCombo.GetDamageBook();
        CheckForResistance();
        damageReceived = currentDamageBook.Sum(x => x.Value);
        if (enemyHealth <= 0)
        {
            Destroy(this);
            print("Enemy Exploded!");
        }
    }

    private void CheckForResistance()
    {
        if (resistance.Equals(MagicType.Empty)) return;
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
