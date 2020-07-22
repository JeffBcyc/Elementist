using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbittingElement : MonoBehaviour
{

    public Transform center;
    public float radius = 2.0f;
    public float rotationSpeed = 80.0f;
    public Vector3 axis = Vector3.up;
    public Vector3 initialPosition;

    private void Start()
    {
        center = FindObjectOfType<ElementAuro>().transform;
        transform.localPosition = initialPosition * radius;
    }


    private void Update()
    {
        ElementRotation(axis, center);
    }

    private void ElementRotation(Vector3 _axis, Transform _center)
    {
        Vector3 rotationVector = new Vector3(-_axis.normalized.x, _axis.normalized.y, _axis.normalized.z);
        transform.RotateAround(_center.position, rotationVector, rotationSpeed * Time.deltaTime);
    }
}
