using UnityEngine;
using UnityEngine.AI;

public class EnemyRoach : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float aggroRange = 20f;

    public Transform playerTransform;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;
        navMeshAgent.stoppingDistance = 0f;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= aggroRange)
        {
            RunAwayFromPlayer();
            navMeshAgent.isStopped = false;
            Debug.Log("YOU'LL NEVA CATCH MEEEE!!!");
        }
        else
        {
            navMeshAgent.isStopped = true;
        }
    }

    void RunAwayFromPlayer()
    {
        Vector3 directionToPlayer = transform.position - playerTransform.position;
        Vector3 runToPosition = transform.position + directionToPlayer;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(runToPosition, out hit, 1f, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (playerTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(playerTransform.position, aggroRange);
        }
    }
}
