using UnityEngine;

public class OrbittingElement : MonoBehaviour
{
    public Vector3 axis = Vector3.up;

    public Transform center;
    public Vector3 initialPosition;
    public float radius = 2.0f;
    public float rotationSpeed = 80.0f;

    private void Start()
    {
        center = FindObjectOfType<ElementAura>().transform;
        transform.localPosition = initialPosition * radius;
    }


    private void Update()
    {
        ElementRotation(axis, center);
    }

    private void ElementRotation(Vector3 _axis, Transform _center)
    {
        var rotationVector = new Vector3(-_axis.normalized.x, _axis.normalized.y, _axis.normalized.z);
        transform.RotateAround(_center.position, rotationVector, rotationSpeed * Time.deltaTime);
    }
}