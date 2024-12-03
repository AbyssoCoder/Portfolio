using UnityEngine;

public class RoachHP : MonoBehaviour
{
    // HP SCRIPT FOR ROACH
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Hit");
        if (other.CompareTag("Gas"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            DieRoach();
        }
    }

    void DieRoach()
    {
        Destroy(gameObject);
    }
}
