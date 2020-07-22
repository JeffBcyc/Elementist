using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ElementBall : MonoBehaviour
{

    Dictionary<ElementType, float> currentElementDamageBook;
    ElementBag currentElementBag;

    float damage;

    TextMesh textMesh;

    private void Awake()
    {
        UpdateDamageBook();
    }

    public void UpdateDamageBook()
    {
        currentElementBag = FindObjectOfType<ElementBag>();
        currentElementDamageBook = currentElementBag.GetDamageBook();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Destroyer")  Destroy(gameObject); 
    }

    public Dictionary<ElementType, float> CurrentElementDamageBook
    {
        get { return currentElementDamageBook; }
    }

}
