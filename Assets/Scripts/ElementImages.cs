using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ElementImages : MonoBehaviour
{

    [SerializeField] ElementSlot thisSlot;
    [SerializeField] ElementBag elementBag;
    [SerializeField] Sprite[] elementSprites;

    ElementType _magic;
    Image _image;


    public ElementSlot ThisSlot
    {
        get { return thisSlot; }
        set { thisSlot = value; }
    }

    private void Start()
    {
        elementBag = FindObjectOfType<ElementBag>();
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _magic = thisSlot.Element;


        string[] arrayNameofSprite = Array.ConvertAll(elementSprites, g => g.name);
        int a = Array.IndexOf(arrayNameofSprite, _magic.ToString());
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
