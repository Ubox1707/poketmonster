using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullPrefab : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    //[SerializeField] Transform rotateModel;
    Transform player;
    float angle;
    float lifeTime;
    int damage;
    float rotateSpeed;
    float radius;
    Vector3 targetSize;

    float currentRadius = 0f;
    float spawnSpeed = 8f;
    bool shrinking = false;
    float timer;

    HashSet<Enemy> enemiesHit = new HashSet<Enemy>();
    public void SetStats(RollingSkull weapon, float initialAngle)
    {
        player = PlayerController.instance.transform;
        angle = initialAngle;
        lifeTime = weapon.weaponStats[weapon.weaponLevel].duration;
        damage = weapon.weaponStats[weapon.weaponLevel].damage;
        rotateSpeed = weapon.weaponStats[weapon.weaponLevel].rotateSpeed;
        radius = weapon.weaponStats[weapon.weaponLevel].rotateRange;
        targetSize = Vector3.one * weapon.weaponStats[weapon.weaponLevel].size;
        timer = 0;
        AudioManager.instance.PlaySound(AudioManager.instance.skullSpawn);
        //transform.localScale = Vector3.one;
    }
    private void Update()
    {
        if (player == null) return;

        if (!shrinking && currentRadius < radius)
        {
            currentRadius = Mathf.MoveTowards(currentRadius, radius, spawnSpeed * Time.deltaTime);
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, spawnSpeed * Time.deltaTime);
        }

       
        angle += rotateSpeed * Mathf.Deg2Rad * Time.deltaTime;
        Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * currentRadius;
        Vector2 center = (Vector2)PlayerController.instance.firePos.position;
        transform.position = Vector2.MoveTowards(transform.position, center + offset, speed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            shrinking = true;
            currentRadius = Mathf.MoveTowards(currentRadius, 0, spawnSpeed * Time.deltaTime);
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, 5* Time.deltaTime);
            if (transform.localScale.x <= 0.05f)
            {
                Destroy(gameObject);
                AudioManager.instance.PlaySound(AudioManager.instance.weaponRespawn);

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
        {
            if (enemy != null && !enemiesHit.Contains(enemy))
            {
                enemy.TakeDamage(damage);
                enemiesHit.Add(enemy);

                AudioManager.instance.PlaySound(AudioManager.instance.skullHit);

            }
            else
            {
                return;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
        {
            if (enemy != null && enemiesHit.Contains(enemy))
            {
                enemiesHit.Remove(enemy);
            }
        }
    }
}
