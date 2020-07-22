using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ElementPivotCenter : MonoBehaviour
{

    [SerializeField] Transform followObject;

    private void Awake()
    {
        //followObject = FindObjectOfType<Mage>().transform;
    }

    private void Update()
    {

        Vector3 auroPosition = new Vector3(
            followObject.position.x,
            1.7f,
            followObject.position.z
            );

        this.transform.position = auroPosition;
    }

}
