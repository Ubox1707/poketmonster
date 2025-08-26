using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : Enemy
{
    [Header("Boss Item")]
    [SerializeField] GameObject[] item;
    [SerializeField] Transform[] itemPos;
    public bool bossDead = false;
    public EnemySpawn enemySpawn;
    [SerializeField] float itemSpeed = 2f;
    [SerializeField] float itemDistance = 0.1f;
    void Rewards()
    {
        if (!PlayerController.instance.isWin)
        {
            for (int i = 0; i < item.Length; i++)
            {
                Instantiate(item[i], itemPos[i].transform.position, Quaternion.identity);
            }

            UIController.instance.LevelUpOpen();

        }
    }

    public override void EnemyDead()
    {
        bossDead = true;  
        enemySpawn.BossDead();
        Rewards();

        Destroy(gameObject);
        Instantiate(deathEffect, transform.position, transform.rotation);
        UIController.instance.bossHpSlider.gameObject.SetActive(false);


    }
}
