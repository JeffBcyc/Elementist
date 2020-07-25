using UnityEngine;

public class ElementControl : MonoBehaviour
{

    ElementBag elementBag;

    private void Awake()
    {
        elementBag = FindObjectOfType<ElementBag>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            elementBag.RotateElementSequence();
        }
    }

}
