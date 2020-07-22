using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ElementCombined : MonoBehaviour
{

    Dictionary<ElementType, float> currentElementDamageBook;
    ElementBag currentElementBag;

    float damage;

    TextMesh textMesh;

    private void Awake()
    {
        currentElementBag = FindObjectOfType<ElementBag>();
        currentElementDamageBook = currentElementBag.GetDamageBook();
        textMesh = GetComponentInChildren<TextMesh>();
        damage = currentElementDamageBook.Sum(x => x.Value);
        textMesh.text = damage.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Destroyer")  Destroy(this); 
    }
}
