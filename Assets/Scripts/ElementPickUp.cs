using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementPickUp : MonoBehaviour
{
    [SerializeField] ElementType elementFromThisBook;
    [SerializeField] ElementBag playerMagicCombo;

    //int elementLimit;


    //var magicQueue;

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
