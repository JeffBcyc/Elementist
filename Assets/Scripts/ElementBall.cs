﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ElementBall : MonoBehaviour
{
    private ElementBag elementBag;
    private Dictionary<ElementType, float> elementBagDamageBook;
    ElementType combinedMagic;
    float combinedMagicDamage;

    private void Awake()
    {
        elementBag = FindObjectOfType<ElementBag>();
        gameObject.name = Time.time.ToString();
        elementBagDamageBook = new Dictionary<ElementType, float>(elementBag.DamageBook);
        combinedMagic = elementBagDamageBook.First().Key;
        combinedMagicDamage = elementBagDamageBook.Sum(x => x.Value);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Destroyer")  Destroy(gameObject); 
    }

    public Dictionary<ElementType,float> DamageBookInTheBall
    { get { return elementBagDamageBook; } }


    public ElementType CombinedMagicType
    {
        get { return combinedMagic; }
    }

    public float CombinedMagicDamage
    {
        get { return combinedMagicDamage; }
    }

}
