using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float health;
    public float maxHealth = 100;

    public GameObject healthBarUI;
    public Slider slider;
    
    public static float speed = 1f;
    private bool isDead = false;
    private Animator animator;
    
    private Transform target;

    private int waypointIndex = 0;
    private GameManager _gameManager;

    private AudioSource _audioSource;
    void Start()
    {
        target = Waypoints.waypoints[0];
        animator = GetComponent<Animator>();
        _gameManager = FindObjectOfType<GameManager>();
        _audioSource = GetComponent<AudioSource>();

        health = maxHealth;
        slider.value = CalculateHealth();
    }

    void Update()
    {
        animator.Play("walk");
        Vector2 direction = target.position - transform.position;
        transform.Translate(direction.normalized * (speed * Time.deltaTime), Space.World);
        look(direction);
        slider.value = CalculateHealth();
    
        if (health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }
        if (Vector2.Distance(transform.position, target.position) <= 0.1f)
        {
            GetNextWaypoint();
        }
        
        if (Vector2.Distance(transform.position, _gameManager.end.position) <= 0.1f)
        {
            _gameManager.FinishGame();
        }
    }
    
    private void look(Vector2 direction)
    {
        if (direction.x >  transform.position.x) // To the right
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

        }
        else if (direction.x < transform.position.x) // To the left
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            
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
        _audioSource.Play();
        if (health <= 0 && !isDead )
        {
            isDead = true;
            Die();
        }
    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }

    void Die()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.UpdateKillCount();
        gameManager.DecreaseMonsterCountInMap();
        Destroy(gameObject);
    }
}
