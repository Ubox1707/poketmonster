using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class AreaWeaponPrefab : MonoBehaviour
{
    //public AreaWeapon weapon;
    public List<Enemy> enemies;
    Vector3 targetSize;
    float timer;
    float counter;
    int damage;
    float period;
    public void SetWeapon(AreaWeapon weapon)
    {
        targetSize = Vector3.one * weapon.weaponStats[weapon.weaponLevel].range;
        timer = weapon.weaponStats[weapon.weaponLevel].duration;
        damage = weapon.weaponStats[weapon.weaponLevel].damage;
        period = weapon.weaponStats[weapon.weaponLevel].periodDamage;

    }
    
    private void Start()
    {
        //weapon = GameObject.Find("AreaWeapon").GetComponent<AreaWeapon>();
        //Destroy(gameObject, weapon.duration);
        //targetSize = Vector3.one * weapon.weaponStats[weapon.weaponLevel].range;
        counter = period;
        transform.localScale = Vector3.zero;
        //timer = weapon.weaponStats[weapon.weaponLevel].duration;
        AudioManager.instance.PlaySound(AudioManager.instance.weaponSpawn);

    }

    private void Update()
    {
        //enemies.RemoveAll(enemies => enemies == null);
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, Time.deltaTime * 4f);

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            targetSize = Vector3.zero;
            if (transform.localScale.x == 0)
            {
                Destroy(gameObject);
                AudioManager.instance.PlaySound(AudioManager.instance.weaponRespawn);
            }
        }
        //Periodically damage enemies
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            //counter = weapon.weaponStats[weapon.weaponLevel].periodDamage;
            counter = period;
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if(enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                }
                else
                {
                    enemies[i].TakeDamage(damage);

                }
            }

           
        }

    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null && !enemies.Contains(enemy))
            {
                enemies.Add(enemy);
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemies.Remove(enemy);
            }

        }
    }
}
