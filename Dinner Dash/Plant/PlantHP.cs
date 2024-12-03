using UnityEngine;

public class PlantHP : MonoBehaviour
{
    // HP SCRIPT FOR PLANT
    public int maxHealth = 50;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.CompareTag("Knife"))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            DiePlant();
        }
    }

    void DiePlant()
    {
        Destroy(gameObject);
    }
}
