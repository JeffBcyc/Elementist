using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    [SerializeField] private RevealText textToDisable;

    [SerializeField] private RevealText textToTrigger;

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