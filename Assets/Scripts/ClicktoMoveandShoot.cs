// ClickToMove.cs
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ClicktoMoveandShoot : MonoBehaviour
{
    RaycastHit hitInfo = new RaycastHit();
    NavMeshAgent agent;

    ElementBag elementBag;
    [SerializeField] float magicSpeed = 10f;
    [SerializeField] float magicLifetime = 2f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        elementBag = FindObjectOfType<ElementBag>();
    }



    void Update()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
        {
            if (Input.GetMouseButtonDown(1)) // right mouse
            {
                agent.destination = hitInfo.point;
            }
            else if (Input.GetMouseButtonDown(0)) // left mouse
            {
                ElementCombined _magic;
                Vector3 direction = (hitInfo.point - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 1f);
                agent.isStopped = true;
                agent.ResetPath();

                _magic = elementBag.CastingSpell();


                _magic.GetComponent<Rigidbody>().velocity = new Vector3(
                    direction.x * magicSpeed,
                    0f,
                    direction.z * magicSpeed);


                Destroy(_magic.gameObject, magicLifetime); // this code doesn't work somehow
                print(_magic.name) ;

            }
        }
        
    }


}