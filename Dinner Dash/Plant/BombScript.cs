using System.Collections;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float dropTime = 3f;
    public float explosionRadius = 5f;
    public float explosionForce = 700f;
    public LayerMask playerLayer;
    public GameObject explosionEffect;
    public float explosionEffectDuration = 2f;
    public Collider BombSplash;
    

    public void StartDropping()
    {
        StartCoroutine(DropAndExplode());
    }

    IEnumerator DropAndExplode()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y - 10, startPosition.z);
        float elapsedTime = 0;

        while (elapsedTime < dropTime)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / dropTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;
        Explode();
    }



    void Explode()
    {

        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        StartCoroutine(DestroyExplosionEffect(explosion));

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, playerLayer);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
      
        Destroy(BombSplash);
        Destroy(gameObject);
    }

    IEnumerator DestroyExplosionEffect(GameObject explosion)
    {
        yield return new WaitForSeconds(explosionEffectDuration);
        Destroy(explosion);
    }
}
