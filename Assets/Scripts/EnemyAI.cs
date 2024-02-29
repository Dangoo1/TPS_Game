using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour

{
    public List<Transform> patrolPoints;

    public PlayerController player;

    public float viewAngle;

    private NavMeshAgent _navMeshAgent;

    private bool _IsPlayerNoticed;



    // Start is called before the first frame update
    private void Start()
    {
        InitComponentLinks();

        PickNewPatrolPoint();
    }
    private void InitComponentLinks()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        NoticedPlayerUpdate();
        ChaseUpdate();
        PatrolUpdate();
    }
    private void NoticedPlayerUpdate()
    {

        var direction = player.transform.position - transform.position;
        _IsPlayerNoticed = false; 
        if (Vector3.Angle(transform.forward, direction) < viewAngle)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, direction, out hit))
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    _IsPlayerNoticed = true;
                }
            }
        }       
    }
    private void PatrolUpdate()
    {

        if (!_IsPlayerNoticed)
        {

            if (_navMeshAgent.remainingDistance == 0)

            {
                PickNewPatrolPoint();
            }

        }

    }  


    private void PickNewPatrolPoint()
    {
        _navMeshAgent.destination = patrolPoints[Random.Range(0, patrolPoints.Count)].position;

    }
    private void ChaseUpdate()
    {
        if (_IsPlayerNoticed)
        {
            _navMeshAgent.destination = player.transform.position;
        }
    }


}
