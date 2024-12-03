using UnityEngine;

public class SpiderHP : MonoBehaviour
{
    // HP SCRIPT FOR SPIDER
    public int maxHealth = 30;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Hit");
        if (other.CompareTag("Ray"))
        {
            TakeDamage(15);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            DieSpider();
        }
    }

    void DieSpider()
    {
        Destroy(gameObject);
    }
}
