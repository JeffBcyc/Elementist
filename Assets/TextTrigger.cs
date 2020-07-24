using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextTrigger : MonoBehaviour
{

    [SerializeField] UpdateText textToTrigger;
    [SerializeField] UpdateText textToDisable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            textToTrigger.gameObject.SetActive(true);

            textToTrigger.StartRollingText();
            try
            {
                textToDisable.gameObject.SetActive(false);
            }
            catch
            {
                Debug.Log("nothing happends");
            }

            // todo: the bug happened because when the book is destroyed, the coroutine is gone.
            // may need to separate the book collider from book

        }

    }

}
