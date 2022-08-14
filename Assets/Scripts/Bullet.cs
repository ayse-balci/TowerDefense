using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    private float speed = 15f;
    private Transform target;
    public Rigidbody2D rb;
    void Start()
    {
        rb.velocity = ( target.position - transform.position ) * speed;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // When bullet hit to an enemy, this function called
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(Random.Range(10, 50));
        }
        Destroy(gameObject);
    }
}