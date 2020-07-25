using UnityEngine;

public class TextTrigger : MonoBehaviour
{

    [SerializeField] RevealText textToTrigger;
    [SerializeField] RevealText textToDisable;

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
            Destroy(gameObject);

        }

    }

}
