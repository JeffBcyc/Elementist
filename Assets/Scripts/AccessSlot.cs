using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class AccessSlot : MonoBehaviour
{

    [SerializeField] ElementSlot thisSlot;
    [SerializeField] Sprite[] elementSprites;
    MagicType _magic;
    Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _magic = thisSlot.GetMagicType();
        string[] arrayNameofSprite = Array.ConvertAll(elementSprites, g => g.name);
        int a = Array.IndexOf(arrayNameofSprite, _magic.ToString());
        _image.sprite = elementSprites[a];

    }

}
