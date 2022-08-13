using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    private float speed = 20f;
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

    public void Hit()
    {
        //Debug.Log("Hitt");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(Random.Range(10, 50));
        }
        Debug.Log("Hit");
        Destroy(gameObject);
    }
}