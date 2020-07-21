using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ElementAuro : MonoBehaviour
{

    [SerializeField] Transform followObject;

    private void Awake()
    {
        followObject = FindObjectOfType<ThirdPersonCharacter>().transform;
    }

    private void Update()
    {
        this.transform.position = followObject.position + Vector3.up * 1.7f;
    }

}
