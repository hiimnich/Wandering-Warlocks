using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public GameObject healthBarPrefab;
    private Slider healthSlider;
    private Transform healthBar;

    public bool showHealthBar = true;

    public GameObject victoryScreen;


    void Start()
    {
        currentHealth = maxHealth;

        GameObject bar = Instantiate(healthBarPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
        healthBar = bar.transform;
        healthBar.SetParent(transform);

        healthSlider = healthBar.GetComponentInChildren<Slider>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        if (!showHealthBar)
        {
            Canvas canvas = healthBar.GetComponentInChildren<Canvas>();
            if (canvas != null)
            {
                canvas.enabled = false;
            }
        }

        if (gameObject.CompareTag("Boss"))
        {
            if (victoryScreen != null)
            {
                victoryScreen.SetActive(false);
            }
        }
    }

    void Update()
    {
        healthBar.position = transform.position + Vector3.up * 2;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (showHealthBar) healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(healthBar.gameObject);
        Destroy(gameObject);

        if (gameObject.CompareTag("Boss"))
        {
            if (victoryScreen != null)
            {
                victoryScreen.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}