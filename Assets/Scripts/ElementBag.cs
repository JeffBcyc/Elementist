﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UIElements;
using UnityEngine.AI;
using System.Linq;

public class ElementBag : MonoBehaviour
{

    public ElementSlot[] elementSlots;
    [SerializeField] float elmentDefaultDamage = 10f;
    [SerializeField] ElementSlot elementSlotPrefab;
    public int elementSlotLimit = 3;
    public Queue<ElementSlot> elementSlotQueue = new Queue<ElementSlot>();

    private Dictionary<ElementType, float> damageBook = new Dictionary<ElementType, float>();
    private MotionController player;

    // generate damagebook according to current elements in the bag
    public Dictionary<ElementType, float> GetDamageBook() 
    {
        return damageBook;
    }

    public Queue<ElementSlot> GetElementSlotQueue() 
    {
        return elementSlotQueue;
    }

    GameObject[] activeParticleSystem;


    private void Awake()
    {
        elementSlots = FindObjectsOfType<ElementSlot>(); // find all elementSlots
        Array.Reverse(elementSlots);
        player = FindObjectOfType<MotionController>();
        InitializeQueues(); // initialize elementSlotQueue
        ReadElements(elementSlots, elmentDefaultDamage); // add all element to the dict
        activeParticleSystem = GameObject.FindGameObjectsWithTag("ElementParticle");
    }


    private void Update()
    {
        activeParticleSystem = GameObject.FindGameObjectsWithTag("ElementParticle");

    }

    private void InitializeQueues()
    {
        for (int i = 0; i < elementSlots.Length; i++)
        {
            elementSlotQueue.Enqueue(elementSlots[i]);
        }
        elementSlotQueue = new Queue<ElementSlot>(elementSlotQueue.Reverse());
    }

    // this old method is hard to work with
    //public void FillInNewElement(ElementType _newElement)
    //{


    //    if (elementSlotQueue.Count < elementSlotLimit)
    //    {

    //        elementSlotQueue.Enqueue(elementSlots[elementSlotQueue.Count]);
    //        elementSlots[elementSlotQueue.Count].Element = _newElement;
    //        elementSlotQueue = new Queue<ElementSlot>(elementSlotQueue.Reverse());
    //    } else if (elementSlotQueue.Count >= elementSlotLimit)
    //    {
    //        ElementSlot _oldElementSlotToRefresh = elementSlotQueue.Dequeue();
    //        _oldElementSlotToRefresh.Element = _newElement;
    //        elementSlotQueue.Enqueue(_oldElementSlotToRefresh);
    //    }

    //    ReadElements(elementSlots, elmentDefaultDamage); 
    //}


    // newer version uses simpler logic:
    public void FillInNewElement(ElementType _newElement)
    {
        int _emptyIndex = elementSlots.Length+1;
        // if there is slot empty, fill in first found slot
        // if all full, fill in the left most slot
        for (int i = 0; i < elementSlots.Length; i++)
        {
            if (elementSlots[i].Element == ElementType.Empty)
            {
                _emptyIndex = i;
                break;
            }
        }
        try
        {
            elementSlots[_emptyIndex].Element = _newElement;
        }
        catch
        {
            elementSlots[0].Element = _newElement;
        }
        ReadElements(elementSlots, elmentDefaultDamage);
    }

    private void ReadElements(ElementSlot[] _elementSlots, float _elementDamage)
    {
        damageBook.Clear();
        foreach (ElementSlot _elementSlot in elementSlots)
        {
            if (_elementSlot.Element == ElementType.Empty) continue;
            try
            {
                damageBook.Add(_elementSlot.Element, _elementDamage);
            }
            catch
            {
                damageBook[_elementSlot.Element] += _elementDamage;
            }
        }
    }

    public void RotateElementSequence()
    {
        ElementType _lastElement = elementSlots[0].Element;
        for (int i = 0; i < elementSlots.Length; i++)
        {
            if (i == (elementSlots.Length - 1))
            {
                elementSlots[i].Element = _lastElement;
            } else
            {
                elementSlots[i].Element = elementSlots[i + 1].Element;
            }
        }
    }

    public void BurnElement()
    {

        for (int i = 0; i < elementSlots.Length; i++)
        {
            if (i == (elementSlots.Length - 1))
            {
                elementSlots[i].Element = ElementType.Empty;
            }
            else
            {
                elementSlots[i].Element = elementSlots[i + 1].Element;
            }
        }
        ReadElements(elementSlots, elmentDefaultDamage);
    }

    public bool LeadElementAvailable()
    {
        return elementSlots[0].Element != ElementType.Empty;
    }
}
