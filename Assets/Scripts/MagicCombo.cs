using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UIElements;
using UnityEngine.AI;
using System.Linq;

public class MagicCombo : MonoBehaviour
{

    public ElementSlot[] elementSlots;
    [SerializeField] float elmentDefaultDamage = 10f;
    [SerializeField] ElementSlot elementSlotPrefab;

    [SerializeField] ElementImages imageLeft;
    [SerializeField] ElementImages imageMiddle;
    [SerializeField] ElementImages imageRight;

    public int elementSlotLimit = 3;
    public Queue<ElementType> elementTypeQueue = new Queue<ElementType>();
    public Queue<ElementSlot> elementSlotQueue = new Queue<ElementSlot>();


    private Dictionary<ElementType, float> damageBook = new Dictionary<ElementType, float>();


    private MotionController player;

    private void Awake()
    {
        elementSlots = FindObjectsOfType<ElementSlot>(); // find all elementSlots
        imageLeft = GameObject.Find("Left").GetComponent<ElementImages>();
        imageMiddle = GameObject.Find("Middle").GetComponent<ElementImages>();
        imageRight = GameObject.Find("Right").GetComponent<ElementImages>();

        Array.Reverse(elementSlots);

        player = FindObjectOfType<MotionController>();
        InitializeQueues(); // initialize elementSlotQueue
        ReadElements(elementSlots, elmentDefaultDamage); // add all element to the dict
        // FillUpElementSlotQueue();
    }

    private void InitializeQueues()
    {
        for (int i = 0; i < elementSlots.Length; i++)
        {
            elementTypeQueue.Enqueue(elementSlots[i].Element);
            elementSlotQueue.Enqueue(elementSlots[i]);
        }
    }

    //private void Update()
    //{
    //    //todo: CastingSpell();
    //    //todo: Check for additional ElementSlot();
    //}

    public void FillInNewElement(ElementType _newElement)
    {
        if (elementTypeQueue.Count < elementSlotLimit)
        {
            elementTypeQueue.Enqueue(_newElement) ;
            elementSlotQueue.Enqueue(elementSlots[elementTypeQueue.Count]);
        } else
        {
            ElementType _oldElementToRefresh = elementTypeQueue.Dequeue();
            ElementSlot _oldElementSlotToRefresh = elementSlotQueue.Dequeue();
            elementTypeQueue.Enqueue(_newElement);


            _oldElementSlotToRefresh.Element = _newElement;
            elementSlotQueue.Enqueue(_oldElementSlotToRefresh);
        }


        int _counter = 0;

        foreach (var i in elementSlotQueue.ToArray())
        {
            elementSlots[_counter] = i;
        }

        imageLeft.ThisSlot = elementSlots[0];
        imageMiddle.ThisSlot = elementSlots[1];
        imageRight.ThisSlot = elementSlots[2];

        ReadElements(elementSlots, elmentDefaultDamage); // update element dict

        //imageLeft.ThisSlot = 


    }


    public Queue<ElementSlot> GetElementSlotQueue()
    {
        return elementSlotQueue;
    }

    private void CastingSpell()    
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("left"); // todo: cast out the spell
        }
        else if (Input.GetMouseButtonDown(1))
        {
            print("right");
        }
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


    public Dictionary<ElementType, float> GetDamageBook()
    {
        return damageBook;
    }


}
