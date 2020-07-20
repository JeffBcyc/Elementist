using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MagicCombo : MonoBehaviour
{

    [SerializeField] ElementSlot[] elementSlots;
    [SerializeField] int elmentDefaultDamage = 10;

    Dictionary<MagicType, int> damageBook = new Dictionary<MagicType, int>();

    private void Start()
    {
        ReadElements(elementSlots, elmentDefaultDamage);
        
        foreach (var item in damageBook)
        {
            //print(item.Key + " : " + item.Value);            
        }
    }

    private void ReadElements(ElementSlot[] _elementSlots, int _elementDamage)
    {
        foreach (ElementSlot _elementSlot in elementSlots)
        {
            if (_elementSlot.GetMagicType() == MagicType.Empty) continue;
            try
            {
                damageBook.Add(_elementSlot.GetMagicType(), _elementDamage);
            }
            catch
            {
                damageBook[_elementSlot.GetMagicType()] += _elementDamage;
            }
        }
    }




}
