using System.Collections;
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
    [SerializeField] ElementCombined elementCombined;
    public int elementSlotLimit = 3;
    public Queue<ElementSlot> elementSlotQueue = new Queue<ElementSlot>();

    private Dictionary<ElementType, float> damageBook = new Dictionary<ElementType, float>();
    private MotionController player;

    
    public Dictionary<ElementType, float> GetDamageBook() {return damageBook;}
    public Queue<ElementSlot> GetElementSlotQueue() {return elementSlotQueue;}

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

        //if (Input.GetMouseButtonDown(0))
        //{
        //    CastingSpell();
        //}
    }

    private void InitializeQueues()
    {
        for (int i = 0; i < elementSlots.Length; i++)
        {
            elementSlotQueue.Enqueue(elementSlots[i]);
        }
    }

    public void FillInNewElement(ElementType _newElement)
    {
        if (elementSlotQueue.Count < elementSlotLimit)
        {
            elementSlotQueue.Enqueue(elementSlots[elementSlotQueue.Count]);
        } else
        {
            ElementSlot _oldElementSlotToRefresh = elementSlotQueue.Dequeue();
            _oldElementSlotToRefresh.Element = _newElement;
            elementSlotQueue.Enqueue(_oldElementSlotToRefresh);
        }

        int _counter = 0;
        foreach (var i in elementSlotQueue.ToArray())
        {
            elementSlots[_counter] = i;
        }

        ReadElements(elementSlots, elmentDefaultDamage); // update element dict
    }

    private void ReadElements(ElementSlot[] _elementSlots, float _elementDamage)
    {
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



    public ElementCombined CastingSpell()    
    {
        ElementCombined magic = Instantiate(elementCombined, transform.position-new Vector3(0f,1.5f,0f), Quaternion.identity);
        return magic;
    }





}
