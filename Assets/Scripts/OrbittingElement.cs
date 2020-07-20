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
        transform.localPosition = initialPosition * radius;
    }


    private void Update()
    {

        Vector3 rotationVector = new Vector3(-axis.normalized.x, axis.normalized.y, axis.normalized.z);
        transform.RotateAround(center.position, rotationVector , rotationSpeed * Time.deltaTime);
        //Vector3 desiredPosition = (transform.position - center.position).normalized * radius + center.position;
        //transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * 2.0f);
    }


}
