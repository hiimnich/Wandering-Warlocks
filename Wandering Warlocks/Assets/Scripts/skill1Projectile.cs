using UnityEngine;

public class skill1Projectile : MonoBehaviour
{
    public float lifetime = 3;

    void Awake()
    {
        Destroy(gameObject, lifetime);
    }

    void onCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
