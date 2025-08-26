using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaWeapon : Weapon
{
    [SerializeField] GameObject prefab;
    float spawnCounter;
   
    private void Update()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = weaponStats[weaponLevel].cooldown;
            GameObject areaPrefab = Instantiate(prefab, transform.position, transform.rotation, transform);
            AreaWeaponPrefab areaWeaponPrefab = areaPrefab.GetComponent<AreaWeaponPrefab>();
            areaWeaponPrefab.SetWeapon(this);
        }
    }
  
    public override void LevelUp()
    {
        if (weaponLevel < weaponStats.Count - 1)
        {
            weaponLevel++;
            //ApplyStats();
        }
    }
    
    //protected override void ApplyStats()
    //{
    //    spawnCounter = weaponStats[weaponLevel].cooldown;
    //}
}
