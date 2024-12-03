using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemySpider : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float shootingRange = 10f;
    public float aggroRange = 20f;
    public LayerMask obstacleMask;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    private Transform playerTransform;
    private bool isShooting = false;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= aggroRange)
        {
            if (distanceToPlayer > shootingRange)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(playerTransform.position);
            }
            else
            {
                if (HasLineOfSight())
                {
                    navMeshAgent.isStopped = true;
                    if (!isShooting)
                    {
                        isShooting = true;
                        StartCoroutine(Shoot());
                    }
                }
                else
                {
                    navMeshAgent.isStopped = false;
                    navMeshAgent.SetDestination(playerTransform.position);
                }
            }
        }
        else
        {
            navMeshAgent.isStopped = true;
            isShooting = false;
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

    IEnumerator Shoot()
    {
        transform.LookAt(playerTransform.position);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        Destroy(bullet, 2f);

        if (bulletRb != null)
        {
            bulletRb.velocity = bullet.transform.forward * bulletSpeed;
        }

        yield return new WaitForSeconds(1f);
        isShooting = false;
    }
}
