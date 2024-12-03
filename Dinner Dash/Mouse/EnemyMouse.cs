using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyMouse : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float attackRange = 2f;
    public float stopDistance = 3f;
    public float aggroRange = 20f;
    public float attackCooldown = 1.5f;
    public int damage = 10;
    public LayerMask obstacleMask;
    public Collider attackHitbox;

    private Transform playerTransform;
    private bool isAttacking = false;
    private NavMeshAgent navMeshAgent;

    private bool Distracted;
    public Transform Cart;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;
        navMeshAgent.stoppingDistance = stopDistance;
        attackHitbox.enabled = false; 
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (Distracted)
        {
            navMeshAgent.SetDestination(Cart.position);
        }
        else
        {
            if (distanceToPlayer <= aggroRange)
            {
                if (distanceToPlayer > attackRange)
                {
                    navMeshAgent.isStopped = false;
                    navMeshAgent.SetDestination(playerTransform.position);
                }
                else
                {
                    if (HasLineOfSight() && !isAttacking)
                    {
                        navMeshAgent.isStopped = true;
                        isAttacking = true;
                        StartCoroutine(Attack());
                    }
                }
            }
            else
            {
                navMeshAgent.isStopped = true;
                isAttacking = false;
            }
        }
    }

    bool HasLineOfSight()
    {
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask))
        {
            return false;
        }

        return true;
    }

    IEnumerator Attack()
    {
        transform.LookAt(playerTransform.position);
        attackHitbox.enabled = true;
        yield return new WaitForSeconds(attackCooldown);
        attackHitbox.enabled = false;

        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cart"))
        {
            Distracted = true;
            Cart = other.transform;
            StartCoroutine(DistractionTime(5));
        }
    }

    IEnumerator DistractionTime(int duration)
    {
        yield return new WaitForSeconds(duration);
        Distracted = false;
        StopAllCoroutines();
    }

    void OnDrawGizmosSelected()
    {
        if (attackHitbox != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackHitbox.bounds.center, attackHitbox.bounds.extents.magnitude);
        }
    }
}
