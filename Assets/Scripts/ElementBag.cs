using System.Collections.Generic;
using UnityEngine;

public class ElementBag : MonoBehaviour
{
    private ParticleSystem _activeParticleSystem;

    private ElementSlot[] _elementSlots;
    [SerializeField] private float elementDefaultDamage = 1f;


    // generate damage book according to current elements in the bag
    // read access only
    public Dictionary<ElementType, float> DamageBook { get; } = new Dictionary<ElementType, float>();

    private void Awake()
    {
        _elementSlots = FindObjectsOfType<ElementSlot>(); // find all elementSlots
        //Array.Reverse(elementSlots);
        FindObjectOfType<MotionController>();
        ReadElements(_elementSlots, elementDefaultDamage); // add all element to the dict
    }


    // newer version uses simpler logic:
    // 1. if there is slot empty, fill in first found slot
    // 2. if all full, fill in the left most slot
    public void FillInNewElement(ElementType newElement)
    {
        var emptyIndex = _elementSlots.Length + 1;
        for (var i = 0; i < _elementSlots.Length; i++)
            if (_elementSlots[i].Element == ElementType.Empty)
            {
                emptyIndex = i;
                break;
            }

        try
        {
            _elementSlots[emptyIndex].Element = newElement;
        }
        catch
        {
            _elementSlots[0].Element = newElement;
        }

        ReadElements(_elementSlots, elementDefaultDamage);
    }

    // no more accumulating damage
    private void ReadElements(ElementSlot[] elementSlots, float elementDamage)
    {
        DamageBook.Clear();
        if (elementSlots[0].Element == ElementType.Fire)
            DamageBook.Add(elementSlots[0].Element, elementDamage * 2);
        else
            DamageBook.Add(elementSlots[0].Element, elementDamage);
    }

    public void RotateElementSequence()
    {
        var lastElement = _elementSlots[0].Element;
        for (var i = 0; i < _elementSlots.Length; i++)
            if (i == _elementSlots.Length - 1)
                _elementSlots[i].Element = lastElement;
            else
                _elementSlots[i].Element = _elementSlots[i + 1].Element;
        ReadElements(_elementSlots, elementDefaultDamage);
    }

    public void BurnElement()
    {
        for (var i = 0; i < _elementSlots.Length; i++)
            if (i == _elementSlots.Length - 1)
                _elementSlots[i].Element = ElementType.Empty;
            else
                _elementSlots[i].Element = _elementSlots[i + 1].Element;
        ReadElements(_elementSlots, elementDefaultDamage);
        //RotateElementSequence();
    }

    public bool LeadElementAvailable()
    {
        return _elementSlots[0].Element != ElementType.Empty;
    }

    public ParticleSystem FirstSlotElement()
    {
        _activeParticleSystem = _elementSlots[0].GetComponentInChildren<ParticleSystem>();
        return _activeParticleSystem;
    }
}