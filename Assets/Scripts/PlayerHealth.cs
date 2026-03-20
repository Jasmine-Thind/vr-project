using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public Image bloodOverlay;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (currentHealth <= 0) return;
        currentHealth -= amount;
        Debug.Log("Player Health: " + currentHealth);

        UpdateOverlay();

        if (currentHealth <= 0)
        {
            Debug.Log("Player is dead");
        }
    }
    void UpdateOverlay()
    {
        float healthPercent = currentHealth / maxHealth;
        float alpha = 1f - healthPercent;
        Debug.Log("Alpha: " + alpha);
        Color c = bloodOverlay.color;
        c.a = alpha;
        bloodOverlay.color = c;
    }
}