using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UIElements;

public class MagicCombo : MonoBehaviour
{

    [SerializeField] ElementSlot[] elementSlots;
    [SerializeField] float elmentDefaultDamage = 10f;


    ThirdPersonCharacter player;

Dictionary<MagicType, float> damageBook = new Dictionary<MagicType, float>();

    private void Awake()
    {
        player = FindObjectOfType<ThirdPersonCharacter>();
    }

    private void Update()
    {
        ReadElements(elementSlots, elmentDefaultDamage);

        //foreach (var item in damageBook)
        //{
        //    //print(item.Key + " : " + item.Value);            
        //}

        
        CastingSpell();

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
            player.Move(Vector3.zero, false);

        }
    }

    private void ReadElements(ElementSlot[] _elementSlots, float _elementDamage)
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


    public Dictionary<MagicType, float> GetDamageBook()
    {
        return damageBook;
    }





}
