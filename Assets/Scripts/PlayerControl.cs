// ClickToMove.cs
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ElementBall))]
[RequireComponent(typeof(ElementBag))]
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerControl : MonoBehaviour
{
    private RaycastHit _hitInfo;
    private NavMeshAgent _agent;
    public ElementBag elementBag;
    
    
    [SerializeField] private float magicSpeed = 10f;
    [SerializeField] private float magicLifetime = 2f;
    [SerializeField] private ElementBall elementBall;
    private Animator _animator;

    private float _agentSpeed;
    private static readonly int SpeedPercent = Animator.StringToHash("speedPercent");

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        elementBag = FindObjectOfType<ElementBag>();

        _animator = GetComponent<Animator>();

    }



    void Update()
    {

        float speedPercent = _agent.velocity.magnitude / _agent.speed;
        _animator.SetFloat(SpeedPercent, speedPercent, 0.1f, Time.deltaTime);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out _hitInfo))
        {
            if (Input.GetMouseButtonDown(1)) // right mouse
            {
                _agent.destination = _hitInfo.point;
            }
            else if (Input.GetMouseButtonDown(0)) // left mouse
            {
                print(_hitInfo.point);
                print(_hitInfo.collider.name);

                ElementBall _magic;
                Vector3 direction = StopMovementAndGetFaceDirection();
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

            if (speedPercent == 0)
            {
                _animator.SetFloat("speedPercent", 0f);
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
        Vector3 direction = (_hitInfo.point - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 1f);
        _agent.isStopped = true;
        _agent.ResetPath();
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
