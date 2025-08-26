using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int weaponLevel;
    [SerializeField] public List<WeaponStats> weaponStats;
    public Sprite weaponIcon;
    public virtual void LevelUp()
    {
        if(weaponLevel < weaponStats.Count - 1)
        {
            weaponLevel++;
            //ApplyStats();
        }

    }
    public bool IsMaxLevel()
    {
        return weaponLevel >= weaponStats.Count - 1;
    }
    //protected virtual void ApplyStats()
    //{

    //}
}

