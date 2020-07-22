using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementPickUp : MonoBehaviour
{
    [SerializeField] ElementType elementFromThisBook;
    [SerializeField] MagicCombo playerMagicCombo;

    //int elementLimit;


    //var magicQueue;

    private void Awake()
    {
        playerMagicCombo = FindObjectOfType<MagicCombo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            playerMagicCombo.FillInNewElement(elementFromThisBook);
        //todo: list not full, fill in magicQueue
        }
    }

}
