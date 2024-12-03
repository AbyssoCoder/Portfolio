using System.Collections;
using UnityEngine;

public class PlantEnemy : MonoBehaviour
{
    public GameObject bombPrefab;
    public float detectionRange = 5f; 
    public float shootInterval = 3f;

    private Transform player;
    private bool playerInRange;
    private Coroutine shootingCoroutine;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= detectionRange && !playerInRange)
        {
            playerInRange = true;
            shootingCoroutine = StartCoroutine(ShootBombs());
        }
        else if (distance > detectionRange && playerInRange)
        {
            playerInRange = false;
            StopCoroutine(shootingCoroutine);
        }
    }

    IEnumerator ShootBombs()
    {
        while (playerInRange)
        {
            SpawnBombAbovePlayer();
            yield return new WaitForSeconds(shootInterval);
        }
    }

    void SpawnBombAbovePlayer()
    {
        Vector3 bombStartPosition = new Vector3(player.position.x, player.position.y + 10, player.position.z); 
        GameObject bomb = Instantiate(bombPrefab, bombStartPosition, Quaternion.identity);
        bomb.GetComponent<BombScript>().StartDropping();
    }
}
