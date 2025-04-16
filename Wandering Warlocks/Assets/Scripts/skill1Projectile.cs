using UnityEngine;
using System.Collections;

public class skill1Projectile : MonoBehaviour
{
    public float lifetime = 3f;
    public float explosionScale = 4f;
    public float explosionDuration = 0.2f;
    public float damageAmount;

    private Vector3 originalScale;
    private Rigidbody rb;

    void Awake()
    {
        originalScale = transform.localScale;
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Damage Amount: " + damageAmount);
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damageAmount);
        }

        StartCoroutine(ExplodeAndDestroy());
    }

    IEnumerator ExplodeAndDestroy()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        float elapsed = 0f;
        Vector3 targetScale = originalScale * explosionScale;

        while (elapsed < explosionDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / explosionDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
    }
}