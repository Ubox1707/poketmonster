using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] protected float speed = 3f;
    [SerializeField] protected int damage;
    [SerializeField] public int health;
    [SerializeField] protected int expValue;
    [SerializeField] protected float pushedTime;


    [Header("Item Random")]
    [SerializeField] public GameObject[] items;

    BossSpawn boss;

    float pushedCounter;
    [Header("Effect")]
    [SerializeField] protected GameObject deathEffect;
    Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    Vector3 direction;
    public int currentHealth;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if(spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found");
        }
        currentHealth = health;
    }
 
    void FixedUpdate()
    {
        if (PlayerController.instance == null)
        {
            return;
        }
        if (PlayerController.instance.gameObject.activeSelf == false)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        ChasePlayer();
       
        boss = GetComponent<BossSpawn>();
        if (boss)
        {
            UIController.instance.UpdateBossHp(boss);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController.instance.TakeDamage(damage);
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        PlayerController.instance.TakeDamage(damage);
    //    }
       
    //}
    void ChasePlayer()
    {
        if (PlayerController.instance.transform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        if (pushedCounter > 0)
        {
            pushedCounter -= Time.deltaTime;
            if (speed > 0)
            {
                speed = -speed;
            }
            if (pushedCounter <= 0)
            {
                speed = Mathf.Abs(speed);
            }

        }


        direction = (PlayerController.instance.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
    }
    public void TakeDamage(int damage)
    {
        if (pushedCounter <= 0)
        {
            pushedCounter = pushedTime;
        }

        currentHealth -= damage;
        pushedCounter = pushedTime/3;
        DamageDisplay.instance.FloatDamage(damage, transform.position);


        if (currentHealth <= 0)
        {
            EnemyDead();
            PlayerController.instance.GetExp(expValue);
            AudioManager.instance.PlayModifiedSound(AudioManager.instance.enemyDie);
        }
    }

    public virtual void EnemyDead() { }
    public virtual void PowerUp() { }

}
