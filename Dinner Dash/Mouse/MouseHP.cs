using UnityEngine;

public class MouseHP : MonoBehaviour
{
    // HP SCRIPT FOR MOUSE
    public int maxHealth = 30;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            DieMouse();
        }
    }

    void DieMouse()
    {
        Destroy(gameObject);
    }
}
