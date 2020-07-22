using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] float enemySight = 5f;
    [SerializeField] Transform target;
    [SerializeField] float enemySpeed = 0.5f;
    [SerializeField] float attackRange = 1.2f;
    NavMeshAgent thisEnemyAI;

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
        float _targetDistance = Vector3.Distance(target.position, transform.position);
        if (_targetDistance < enemySight && _targetDistance >= attackRange)
        {
            thisEnemyAI.SetDestination(target.position);
        } 
    }

}
