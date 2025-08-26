using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPrefab : MonoBehaviour
{
    public int damage;
    int lifeTime = 3;
    [SerializeField] float rotateSpeed = 360f;
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    private void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Enemy enemy = collision.GetComponent<Enemy>();

    //    if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
    //    {
    //        if (enemy != null)
    //        {
    //            enemy.TakeDamage(damage);
    //            AudioManager.instance.PlaySound(AudioManager.instance.bulletHit);
    //            Destroy(gameObject);
    //        }

    //    }
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            if (enemy != null)
            {
                enemy.TakeDamage(damage);

            }

        }
    }
}
