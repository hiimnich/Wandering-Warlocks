using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public GameObject healthBarPrefab;
    private Slider healthSlider;
    private Transform healthBar;

    void Start()
    {
        currentHealth = maxHealth;

        GameObject bar = Instantiate(healthBarPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
        healthBar = bar.transform;

        healthSlider = healthBar.GetComponentInChildren<Slider>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    void LateUpdate()
    {
        if (healthBar != null)
        {
            healthBar.position = transform.position + Vector3.up * 2;
            healthBar.rotation = Quaternion.Euler(90, 0, 0);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(healthBar.gameObject);
        Destroy(gameObject);
    }
}
