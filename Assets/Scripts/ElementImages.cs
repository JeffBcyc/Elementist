using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElementImages : MonoBehaviour
{

    [SerializeField] ElementSlot thisSlot;
    [SerializeField] ElementBag elementBag;
    [SerializeField] Sprite[] elementSprites;
    TMP_Text damageAmount;

    ElementType _magic;
    Image _image;


    public ElementSlot ThisSlot
    {
        get { return thisSlot; }
        set { thisSlot = value; }
    }

    private void Start()
    {
        damageAmount = GetComponentInChildren<TMP_Text>();
        elementBag = FindObjectOfType<ElementBag>();
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _magic = thisSlot.Element;

        if (_magic == ElementType.Fire)
        {
            damageAmount.text = "2";
        } else if (_magic == ElementType.Empty)
        {
            damageAmount.text = "0";
        }
        else
        {
            damageAmount.text = "1";
        }
        
        string[] arrayNameofSprite = Array.ConvertAll(elementSprites, g => g.name);
        int a = Array.IndexOf(arrayNameofSprite, _magic.ToString());
        //print(thisSlot.name + ":" + a);
        try
        {
            _image.sprite = elementSprites[a];
        }
        catch
        {
            Debug.Log(ThisSlot.name + " is empty, no picture to display");
        }

    }

}
