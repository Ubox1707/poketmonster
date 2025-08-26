using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingSkull : Weapon
{
    [SerializeField] GameObject prefab;
    List<GameObject> skulls = new List<GameObject>();
    float spawnCounter;
    private void Update()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = weaponStats[weaponLevel].cooldown;
            
            for(int i=0; i< weaponStats[weaponLevel].skullAmount; i++)
            {
               float angle = i*Mathf.PI* 2 / weaponStats[weaponLevel].skullAmount;
                GameObject skull = Instantiate(prefab, PlayerController.instance.firePos.position, Quaternion.identity, transform);
                SkullPrefab skullPrefab = skull.GetComponent<SkullPrefab>();
                skullPrefab.SetStats(this, angle);
                skulls.Add(skull);

            }

            //skull.transform.localScale = Vector3.one* weaponStats[weaponLevel].size;
            //transform.Rotate(PlayerController.instance.firePos.position.x, PlayerController.instance.firePos.position.y, weaponStats[weaponLevel].rotateSpeed * Time.deltaTime);
        }
    }

   
}
