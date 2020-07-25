using UnityEngine;

public class Billboard : MonoBehaviour
{

    [SerializeField] Transform cam;

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }


}
