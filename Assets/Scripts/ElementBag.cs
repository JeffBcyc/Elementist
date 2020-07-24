using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UIElements;
using UnityEngine.AI;
using System.Linq;
using System.Net;

public class ElementBag : MonoBehaviour
{

    private ElementSlot[] elementSlots;
    [SerializeField] float elmentDefaultDamage = 10f;
    private int elementSlotLimit = 3;
    private Dictionary<ElementType, float> damageBook = new Dictionary<ElementType, float>();
    private MotionController player;
    private ParticleSystem activeParticleSystem;


    // generate damagebook according to current elements in the bag
    // read access only
    public Dictionary<ElementType, float> DamageBook
    {
       get {return damageBook; }
    }



    private void Awake()
    {
        elementSlots = FindObjectsOfType<ElementSlot>(); // find all elementSlots
        Array.Reverse(elementSlots);
        player = FindObjectOfType<MotionController>();
        ReadElements(elementSlots, elmentDefaultDamage); // add all element to the dict
    }


    // newer version uses simpler logic:
    // 1. if there is slot empty, fill in first found slot
    // 2. if all full, fill in the left most slot
    public void FillInNewElement(ElementType _newElement)
    {
        int _emptyIndex = elementSlots.Length+1;
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

    // no more accumulating damage
    private void ReadElements(ElementSlot[] _elementSlots, float _elementDamage)
    {
        damageBook.Clear();
        if (_elementSlots[0].Element == ElementType.Fire)
        {
            damageBook.Add(_elementSlots[0].Element, _elementDamage * 2);
        } else
        {
            damageBook.Add(_elementSlots[0].Element, _elementDamage);
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
        ReadElements(elementSlots, elmentDefaultDamage);
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
        //RotateElementSequence();
    }

    public bool LeadElementAvailable()
    {
        return elementSlots[0].Element != ElementType.Empty;
    }

    public ParticleSystem FirstSlotElement()
    {
        activeParticleSystem = elementSlots[0].GetComponentInChildren<ParticleSystem>();
        return activeParticleSystem ;
    }


}
