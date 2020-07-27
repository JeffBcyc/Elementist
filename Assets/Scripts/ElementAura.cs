using UnityEngine;

public class ElementAura : MonoBehaviour
{
    [SerializeField] private Transform followObject;

    private void Awake()
    {
        //followObject = FindObjectOfType<Mage>().transform;
    }

    private void Update()
    {
        var position = followObject.position;
        var auraPosition = new Vector3(
            position.x,
            1.7f,
            position.z
        );

        transform.position = auraPosition;
    }
}