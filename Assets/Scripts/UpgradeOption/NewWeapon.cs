using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName ="Upgrade/New Weapon")]
public class NewWeapon : UpgradeOption
{
    public Weapon newWeaponPrefab;
    public string weaponName;

    //public override string description => weaponStat;
    public override string description
    {
        get
        {
            var player = PlayerController.instance;
            var weapon = player.weapons.Find(w => w.name.Contains(weaponName));
            if (weapon != null)
            {
                return weapon.weaponStats[weapon.weaponLevel + 1].description;
            }
            return "New Weapon";
        }
    }
    public override bool CanBeCalled(PlayerController player)
    {
        Weapon weapon = player.weapons.Find(w => w.name.Contains(weaponName));
        return weapon == null;
    }
    public override void UpgradeApply(PlayerController player)
    {
        Weapon weapon = Instantiate(newWeaponPrefab, player.transform);
        player.weapons.Add(weapon);
    }
}
