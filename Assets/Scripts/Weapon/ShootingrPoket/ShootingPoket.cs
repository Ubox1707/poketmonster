using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPoket : Weapon
{
    [SerializeField] GameObject bulletPrefab;
    float spawnCounter;
    [SerializeField] float angle;

    private void Start()
    {
        angle = Random.Range(3f, 15f);

    }
    private void Update()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = weaponStats[weaponLevel].cooldown;
            int count = weaponStats[weaponLevel].bulletAmount;
            for (int i = 0; i < count; i++) 
            {
                Vector2 dir = PlayerController.instance.previousDir;

                Vector3 offset = dir.normalized * 1f;
                Vector3 spawnPos = PlayerController.instance.firePos.position + offset;
                //float angle = i * 360 / count;
                //Vector3 dir = Quaternion.Euler(0, 0, angle) * Vector3.right;
                float angleOffset = (i - (count - 1) / 2f) * angle;
                Vector2 rotateDir = Quaternion.Euler(0, 0, angleOffset) * dir;

                AudioManager.instance.PlaySound(AudioManager.instance.bulletHit);

                GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = rotateDir* weaponStats[weaponLevel].bulletSpeed;
                bullet.GetComponent<BulletPrefab>().damage = weaponStats[weaponLevel].damage;
            }
        }
    }
    
  
}
