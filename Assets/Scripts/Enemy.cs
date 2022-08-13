using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public static float speed = 3f;

    private Transform target;
    private int waypointIndex = 0;
    
    void Start()
    {
        target = Waypoints.waypoints[0];
    }

    void Update()
    {
        Vector2 direction = target.position - transform.position;
        transform.Translate(direction.normalized * (speed * Time.deltaTime), Space.World);

        if (Vector2.Distance(transform.position, target.position) <= 0.1f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Dead");
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.UpdateKillCount();
        Destroy(gameObject);
    }
}
