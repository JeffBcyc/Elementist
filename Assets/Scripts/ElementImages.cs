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
    [SerializeField] ElementBag playerMagicCombo;
    [SerializeField] Sprite[] elementSprites;

    [SerializeField] int Queuenumber;

    Queue<ElementSlot> currentSlotQueue;
    ElementType _magic;
    Image _image;

    List<ElementSlot> currentQueueList = new List<ElementSlot>();

    public ElementSlot ThisSlot
    {
        get { return thisSlot; }
        set { thisSlot = value; }
    }

    private void Start()
    {
        playerMagicCombo = FindObjectOfType<ElementBag>();
        currentSlotQueue = playerMagicCombo.GetElementSlotQueue();
        currentQueueList = currentSlotQueue.ToList();
        thisSlot = currentQueueList[Queuenumber];
        //thisSlot = playerMagicCombo.elementSlots[correspondingSlotIndex];
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        currentSlotQueue = playerMagicCombo.GetElementSlotQueue();
        currentQueueList = currentSlotQueue.ToList();
        currentQueueList.Reverse();
        thisSlot = currentQueueList[Queuenumber];

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
