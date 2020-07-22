using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ElementImages : MonoBehaviour
{

    [SerializeField] ElementSlot thisSlot;
    [SerializeField] int correspondingSlotIndex;
    [SerializeField] MagicCombo playerMagicCombo;
    [SerializeField] Sprite[] elementSprites;


    ElementSlot _targetSlotToUpdate;
    ElementType _magic;
    Image _image;


    public ElementSlot ThisSlot
    {
        get { return thisSlot; }
        set { thisSlot = value; }
    }

    private void Start()
    {
        playerMagicCombo = FindObjectOfType<MagicCombo>();
        thisSlot = playerMagicCombo.elementSlots[correspondingSlotIndex];
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _magic = thisSlot.Element;
        string[] arrayNameofSprite = Array.ConvertAll(elementSprites, g => g.name);
        int a = Array.IndexOf(arrayNameofSprite, _magic.ToString());
        _image.sprite = elementSprites[a];

    }

    //public void UpdateElementImages()
    //{
    //    Queue<ElementSlot> _currentSlotQueue = playerMagicCombo.GetElementSlotQueue();
    //    print(correspondingSlotIndex);
    //    for (int i = -1; i < correspondingSlotIndex; i++)
    //    {
    //        _targetSlotToUpdate = _currentSlotQueue.Dequeue();
    //    }
    //    print(_targetSlotToUpdate.Element.ToString());
    //}

}
