using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPrefab : Enemy
{
    [Header("Power Up")]
    [SerializeField] float upSpeed;
    [SerializeField] int upDamage;
    [SerializeField] public int upHealth;
    [SerializeField] int upExpValue;
    [SerializeField] float upPushedTime;

    bool isGround = false;
    bool isBound = false;
    bool isBackground = false;
    bool isObstacle = false;

    int powerUp = 0;

    public override void EnemyDead()
    {
        ColliderCheck();

        Destroy(gameObject);
        Instantiate(deathEffect, transform.position, transform.rotation);

        if (isGround && !isBound && !isBackground && !isObstacle)
        {
            CreateItem();
        }

    }
    void CreateItem()
    {
        if (Random.Range(0f, 1f) > 0.5f)
        {
            Instantiate(items[Random.Range(0, items.Length)], transform.position, Quaternion.identity);
        }
        else { return; }
    }
    void ColliderCheck()
    {
        isGround = false;
        isBound = false;
        isBackground = false;
        isObstacle = false;
        Vector2 size = new Vector2(0.1f, 0.1f);
        Collider2D[] collides = Physics2D.OverlapBoxAll(transform.position, size, 0f);
        foreach (var col in collides)
        {

            if (col.CompareTag("Ground"))
            {
                isGround = true;
            }
            if (col.CompareTag("Boundaries"))
            {
                isBound = true;
            }
            if (col.CompareTag("Obstacle"))
            {
                isObstacle = true;
            }
            if (col.CompareTag("Background"))
            {
                if (!isGround)
                {
                    isBackground = true;
                }

            }

        }
    }

    public override void PowerUp()
    {
        powerUp++;
        currentHealth += upHealth;
        health += upHealth;
        damage += upDamage;
        expValue += upExpValue;
        pushedTime -= upPushedTime;
        pushedTime = Mathf.Clamp(pushedTime, 0.1f, 0.55f);
        speed += upSpeed;
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, powerUp * 0.2f);
        }
        else
        {
            Debug.LogError("SpriteRenderer not found");
        }
    }
}
