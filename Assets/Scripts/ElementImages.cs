using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElementImages : MonoBehaviour
{
    private Image _image;

    private ElementType _magic;
    private TMP_Text damageAmount;
    [SerializeField] private ElementBag elementBag;
    [SerializeField] private Sprite[] elementSprites;

    [SerializeField] private ElementSlot thisSlot;


    public ElementSlot ThisSlot
    {
        get => thisSlot;
        set => thisSlot = value;
    }

    private void Start()
    {
        damageAmount = GetComponentInChildren<TMP_Text>();
        elementBag = FindObjectOfType<ElementBag>();
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _magic = thisSlot.Element;

        if (_magic == ElementType.Fire)
            damageAmount.text = "2";
        else if (_magic == ElementType.Empty)
            damageAmount.text = "0";
        else
            damageAmount.text = "1";

        var arrayNameofSprite = Array.ConvertAll(elementSprites, g => g.name);
        var a = Array.IndexOf(arrayNameofSprite, _magic.ToString());
        //print(thisSlot.name + ":" + a);
        try
        {
            _image.sprite = elementSprites[a];
        }
        catch
        {
            Debug.Log(ThisSlot.name + " is empty, no picture to display");
        }
    }
}