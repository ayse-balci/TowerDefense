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

    private AudioSource _audioSource;
    
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); // prevent calling UpdateTarget in every frame
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (target == null)
            return;
        
        // Update target direction by nearest enemy (target)
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction);
        rotationPart.rotation = Quaternion.RotateTowards(rotationPart.rotation, lookRotation, 3f );
        
        // Fire in every second
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

    }

    void UpdateTarget()
    {
        // Function update target by the distances of all enemies to tank
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
        _audioSource.Play();

    }

}
