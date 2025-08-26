using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Upgrade/Weapon Upgrade")]
public class WeaponUpgrade : UpgradeOption
{
    public string weaponName;

    public override void UpgradeApply(PlayerController player)
    {
        Weapon weapon = player.weapons.Find(w => w.name.Contains(weaponName));

        if(weapon != null && !weapon.IsMaxLevel())
        {
            weapon.LevelUp();
        }
        else
        {
            Debug.LogWarning("Weapon not found");
        }
    }
    public override bool CanBeCalled(PlayerController player)
    {
        Weapon weapon = player.weapons.Find(w => w.name.Contains(weaponName));
        return weapon != null && !weapon.IsMaxLevel();
    }
    public override string description
    {
        get
        {
            var player = PlayerController.instance;
            var weapon = player.weapons.Find(w => w.name.Contains(weaponName));
            if(weapon != null && !weapon.IsMaxLevel())
            {
                return weapon.weaponStats[weapon.weaponLevel + 1].description;
            }
            return "Upgrade Weapon";
        }
    }
}
