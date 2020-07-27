using System;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{

    [SerializeField] private RevealText textToTrigger;
    [SerializeField] private RevealText textToDisable;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        textToTrigger.gameObject.SetActive(true);
        textToTrigger.StartRollingText();
        try
        {
            textToDisable.gameObject.SetActive(false);
        }
        catch
        {
            Debug.Log("nothing happens");
        }
        Destroy(gameObject);

    }

    
}
