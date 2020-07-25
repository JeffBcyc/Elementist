// ClickToMove.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(NavMeshAgent))]
public class ClicktoMoveandShoot : MonoBehaviour
{
    RaycastHit hitInfo = new RaycastHit();
    NavMeshAgent agent;

    ElementBag elementBag;
    [SerializeField] float magicSpeed = 10f;
    [SerializeField] float magicLifetime = 2f;
    [SerializeField] ElementBall elementBall;
    Animator animator;

    private float agentSpeed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        elementBag = FindObjectOfType<ElementBag>();

        animator = GetComponent<Animator>();

    }



    void Update()
    {

        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, 0.1f, Time.deltaTime);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
        {
            if (Input.GetMouseButtonDown(1)) // right mouse
            {
                agent.destination = hitInfo.point;
            }
            else if (Input.GetMouseButtonDown(0)) // left mouse
            {
                print(hitInfo.point);
                print(hitInfo.collider.name);

                ElementBall _magic;
                Vector3 direction = StopMovementAndGetFaceDirection();
                if (elementBag.LeadElementAvailable())
                {
                    _magic = GenerateSpell();
                    CastTo(_magic, direction);
                } else
                {
                    print("Press space to assign a lead element to be able to cast spell");
                }
            }

            if (speedPercent == 0)
            {
                animator.SetFloat("speedPercent", 0f);
            }

        }


    }

    private void DisplayDamageDictionary()
    {
        
    }

    private void CastTo(ElementBall _magic, Vector3 direction)
    {
        _magic.GetComponent<Rigidbody>().velocity = new Vector3(
            direction.x * magicSpeed,
            0f,
            direction.z * magicSpeed);
    }

    private Vector3 StopMovementAndGetFaceDirection()
    {
        Vector3 direction = (hitInfo.point - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 1f);
        agent.isStopped = true;
        agent.ResetPath();
        return direction;
    }

    public ElementBall GenerateSpell()
    {
        ParticleSystem _mainParticle = elementBag.FirstSlotElement();

        TextMesh _textMesh;
        float _damage;
        Vector3 _spellPosition = new Vector3(transform.position.x, 0f, transform.position.z);
        ElementBall magic = Instantiate(elementBall, _spellPosition, Quaternion.identity);
        


        elementBag.BurnElement();
        _textMesh = magic.GetComponentInChildren<TextMesh>();
        _damage = magic.DamageBookInTheBall.Sum(x => x.Value);
        _textMesh.text = _damage.ToString();
        return magic;

    }

   

}
