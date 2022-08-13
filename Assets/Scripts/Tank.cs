using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public Transform target;
    public float range = 15f;
    public Transform rotationPart;
    
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    
    public Transform gun; 
    public Transform bulletPrefab;
    
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
    {
        if (target == null)
            return;
        
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction);
        rotationPart.rotation = Quaternion.RotateTowards(rotationPart.rotation, lookRotation, 3f );

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (var enemy in enemies)
        {
            
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    
    void Shoot()
    {
        GameObject bulletObject = (GameObject) Instantiate(bulletPrefab, new Vector3(gun.position.x, gun.position.y, -1f), gun.rotation).gameObject;
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.SetTarget(target);

    }

}
