using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn instance;
    [SerializeField] Transform minPos;
    [SerializeField] Transform maxPos;
    [SerializeField] GameObject[] bossPrefab;
    //[SerializeField] Transform bossPos;
    [SerializeField] float bossSpawnTime = 30f;
    [SerializeField] float nextSpawnBoss = 30f;

    BossSpawn boss;
    int count = 0;
    bool bossSpawned = false;

    Vector2 spawnPos;
    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefab;
        public int enemyNumber;
        public int enemyCount;
        public float spawnInterval;
        public float spawnTime;

    }


    public List<Wave> waves;
    public int waveIndex;
  
    
    private void Update()
    {
        if (PlayerController.instance.gameObject.activeSelf)
        {
            waves[waveIndex].spawnTime += Time.deltaTime;
            if (waves[waveIndex].spawnTime >= waves[waveIndex].spawnInterval)
            {
                waves[waveIndex].spawnTime = 0f;

                SpawnEnemy();
            }
            if (!bossSpawned && GameManager.instance.remainingTime >= bossSpawnTime)
            {

                SpawnBoss();
                bossSpawned = true;
            }

            spawnPos = new Vector2(PlayerController.instance.transform.position.x, PlayerController.instance.transform.position.y+5f);
        }


    }

    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(waves[waveIndex].enemyPrefab, RandomPos(), transform.rotation); 
        Enemy enemy = newEnemy.GetComponent<Enemy>();
        if(enemy != null)
        {
            for(int i=0; i<GameManager.instance.GetPowerUpCount(); i++)
            {
                enemy.PowerUp();
            }
        }
        waves[waveIndex].enemyCount++;
        if (waves[waveIndex].enemyCount >= waves[waveIndex].enemyNumber)
        {
            waves[waveIndex].enemyCount = 0;

            if (waves[waveIndex].spawnInterval >= 0.3f)
            {
                waves[waveIndex].spawnInterval *= 0.9f;
            }

            waveIndex++;
        }
        if (waveIndex >= waves.Count)
        {
            waveIndex = 0;
        }
    }
    void SpawnBoss()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.bossIntro);
        GameManager.instance.textFadeIn = true;
        GameObject bossObj = Instantiate(bossPrefab[count], spawnPos, Quaternion.identity);
        boss = bossObj.GetComponent<BossSpawn>();
        boss.enemySpawn = this;
        count += 1;
        if(count == 4)
        {
            UIController.instance.bossCount.text = "Final Boss";

        }
        else
            UIController.instance.bossCount.text = "Boss " + count.ToString();

    }
    public void BossDead()
    {
        bossSpawned = false;
        boss = null;
        NextWave();
    }
    void NextWave()
    {
        if (count == 4)
        {
            PlayerController.instance.PlayerWin();
        }
        bossSpawnTime = GameManager.instance.remainingTime + nextSpawnBoss;
        SpawnEnemy();

    }

    Vector2 RandomPos()
    {
        Vector2 spawnPos;
        if (Random.Range(0f, 1f) > 0.5f)
        {
            spawnPos.x = Random.Range(minPos.position.x, maxPos.position.x);
            if (Random.Range(0f, 1f) > 0.5f)
            {
                spawnPos.y = minPos.position.y;
            }
            else
            {
                spawnPos.y = maxPos.position.y;
            }
        }
        else
        {
            spawnPos.y = Random.Range(minPos.position.y, maxPos.position.y);
            if (Random.Range(0f, 1f) > 0.5f)
            {
                spawnPos.x = minPos.position.x;
            }
            else
            {
                spawnPos.x = maxPos.position.x;
            }
        }
        return spawnPos;
    }
}
