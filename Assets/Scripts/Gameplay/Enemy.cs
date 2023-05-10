using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed;
    float _speed;   
    public float Speed
    {
        get { return _speed; }
        set
        {
            if (value >= 0)
            {
                _speed = value;
            }
        }
    }

    public int destroyValue = 50;
    public GameObject deathEffectPrefab;
    public HealthUI healthUI;
    [SerializeField]
    float startHealth = 100;
    [field:SerializeField] public float Health { get; private set; }
    bool isDead = false;

    float timer = 0;
    bool startCount = false;

    private void Start()
    {
        Health = startHealth;
    }

    private void Update()
    {
            if (startCount && !isDead)
        {
            timer += Time.deltaTime;
        }
    }

    public void SlowDown(float amount)
    {
        Speed = Speed * (100 - amount) / 100;
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        if (!startCount)
        {
            startCount = true;
        }

        Health -= amount;
        healthUI.ChangeHealthUI(Health, startHealth);

        if (Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        PlayerStats.Money += destroyValue;

        GameObject deathEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        Spawner.EnemiesAlive--;

        Destroy(deathEffect, 2);
        Destroy(gameObject);
    }
}