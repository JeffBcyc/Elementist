// ClickToMove.cs

using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ElementBall))]
[RequireComponent(typeof(ElementBag))]
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerControl : MonoBehaviour
{
    private static readonly int SpeedPercent = Animator.StringToHash("speedPercent");
    private NavMeshAgent _agent;

    private float _agentSpeed;
    private Animator _animator;
    private Camera _camera;
    private RaycastHit _hitInfo;
    private ElementBall _magic;
    public ElementBag elementBag;
    [SerializeField] private ElementBall elementBall;
    [SerializeField] private float magicLifetime = 2f;


    [SerializeField] private float magicSpeed = 10f;


    private void Start()
    {
        _camera = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }


    private void Update()
    {
        var speedPercent = _agent.velocity.magnitude / _agent.speed;
        _animator.SetFloat(SpeedPercent, speedPercent, 0.1f, Time.deltaTime);

        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray.origin, ray.direction, out _hitInfo)) return;
        if (Input.GetMouseButtonDown(1)) // right mouse
        {
            _agent.destination = _hitInfo.point;
        }
        else if (Input.GetMouseButtonDown(0)) // left mouse
        {
            print(_hitInfo.point);
            print(_hitInfo.collider.name);

            var direction = StopMovementAndGetFaceDirection();
            if (elementBag.LeadElementAvailable())
            {
                _magic = GenerateSpell();
                CastTo(_magic, direction);
            }
            else
            {
                print("Press space to assign a lead element to be able to cast spell");
            }
        }

        if (Math.Abs(speedPercent) < Mathf.Epsilon) _animator.SetFloat(SpeedPercent, 0f);
    }

    private void DisplayDamageDictionary()
    {
    }

    private void CastTo(ElementBall magic, Vector3 direction)
    {
        magic.rigidbody.velocity = new Vector3(
            direction.x * magicSpeed,
            0f,
            direction.z * magicSpeed);
    }

    private Vector3 StopMovementAndGetFaceDirection()
    {
        var direction = (_hitInfo.point - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 1f);
        _agent.isStopped = true;
        _agent.ResetPath();
        return direction;
    }

    private ElementBall GenerateSpell()
    {
        var currentTransform = transform.position;
        var spellPosition = new Vector3(currentTransform.x, 0f, currentTransform.z);
        var mainParticle = elementBag.FirstSlotElement();
        var magic = Instantiate(elementBall, spellPosition, Quaternion.identity);
        var textMesh = magic.GetComponentInChildren<TextMesh>();
        var damage = magic.DamageBookInTheBall.Sum(x => x.Value);


        elementBag.BurnElement();
        return magic;
    }
}