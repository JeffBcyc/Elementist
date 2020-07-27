using UnityEngine;

public class ElementControl : MonoBehaviour
{
    private ElementBag elementBag;

    private void Awake()
    {
        elementBag = FindObjectOfType<ElementBag>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) elementBag.RotateElementSequence();
    }
}