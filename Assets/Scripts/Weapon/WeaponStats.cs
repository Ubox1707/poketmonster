using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponStats
{
    [Header("Common")]
    public float cooldown;
    public int damage;

    [Header("Area Weapon & RollingSkull")]
    public float duration;

    [Header("Area Weapon")]
    public float range;
    public float periodDamage;

    [Header("Shooting Poket")]
    public float bulletSpeed;
    public int bulletAmount;

    [Header("Rolling Skull")]
    public float rotateSpeed;
    public int skullAmount;
    public float rotateRange;
    public float size;

    [Header("Description")]
    public string description;

}

