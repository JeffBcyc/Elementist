using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float attackRange = 1.2f;

    [SerializeField] private float enemySight = 5f;
    [SerializeField] private float enemySpeed = 0.5f;
    [SerializeField] private Transform target;
    private NavMeshAgent thisEnemyAI;

    private void Awake()
    {
        target = FindObjectOfType<MotionController>().transform;
        thisEnemyAI = GetComponent<NavMeshAgent>();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, enemySight);
    }

    private void Update()
    {
        var _targetDistance = Vector3.Distance(target.position, transform.position);
        if (_targetDistance < enemySight && _targetDistance >= attackRange) thisEnemyAI.SetDestination(target.position);
    }
}