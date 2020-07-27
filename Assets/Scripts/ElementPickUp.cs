using System;
using UnityEngine;

public class ElementPickUp : MonoBehaviour
{
    private Color color;
    [SerializeField] private ElementType elementFromThisBook;
    [SerializeField] private ElementBag playerMagicCombo;

    public ElementType ElementFromThisBook => elementFromThisBook;

    [Obsolete]
    private void Awake()
    {
        playerMagicCombo = FindObjectOfType<ElementBag>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            playerMagicCombo.FillInNewElement(elementFromThisBook);
            Destroy(gameObject);
        }
    }
}