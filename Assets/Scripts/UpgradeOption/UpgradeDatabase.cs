using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDatabase : MonoBehaviour
{
    public static UpgradeDatabase instance;

    public List<UpgradeOption> allUpgrades;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<UpgradeOption> GetRandomUpgrade(int weaponCardCount, int supportCardCount)
    {
        //bool unlockedWeapon = false;

        List<UpgradeOption> newWeaponUnloked = new List<UpgradeOption>();
        List<UpgradeOption> weaponUpgraded = new List<UpgradeOption>();
        List<UpgradeOption> supportCard = new List<UpgradeOption>();


        foreach (var upgrade in allUpgrades)
        {
            if (upgrade is NewWeapon newWeapon)
            {
                if (newWeapon.CanBeCalled(PlayerController.instance))
                {
                    newWeaponUnloked.Add(upgrade);
                    //unlockedWeapon = true;
                }
            }

            else if (upgrade is WeaponUpgrade weaponUpgrade)
            {
                //var weapon = PlayerController.instance.weapons.Find(w=>w.name.Contains(weaponUpgrade.weaponName));
                //if(weapon != null && !weapon.IsMaxLevel())
                //{
                //    upgradeAvailable.Add(upgrade);
                //}
                if (/*unlockedWeapon  newWeaponUnloked.Find(w => w.name.Contains(upgrade.name)) ||*/ weaponUpgrade.CanBeCalled(PlayerController.instance))
                {
                    weaponUpgraded.Add(upgrade);
                }
            }
            else
            {
                supportCard.Add(upgrade);
            }
        }

        List<UpgradeOption> weaponRand = new List<UpgradeOption>();
        weaponRand.AddRange(newWeaponUnloked);
        weaponRand.AddRange(weaponUpgraded);

        List<UpgradeOption> selectedUpgrades = new List<UpgradeOption>();

        //Random Weapon Card
        for (int i = 0; i < weaponCardCount; i++)
        {
            UpgradeOption rand = null;
            if (weaponRand.Count > 0)
            {
                rand = weaponRand[Random.Range(0, weaponRand.Count)];
                selectedUpgrades.Add(rand);
                weaponRand.Remove(rand);
            }
        }
        //Random Support Card
        for (int i = 0; i < supportCardCount; i++)
        {
            if (supportCard.Count > 0)
            {
                UpgradeOption rand = supportCard[Random.Range(0, supportCard.Count)];
                selectedUpgrades.Add(rand);
                supportCard.Remove(rand);
            }
        }

        while (selectedUpgrades.Count < 4)
        {
            List<UpgradeOption> subCards = new List<UpgradeOption>();
            subCards.AddRange(newWeaponUnloked);
            subCards.AddRange(weaponUpgraded);
            subCards.AddRange(supportCard);

            if (subCards.Count == 0) break;

            var rand = subCards[Random.Range(0, subCards.Count)];
            selectedUpgrades.Add(rand);

            weaponRand.Remove(rand);
            supportCard.Remove(rand);
        }
        return selectedUpgrades;
    }
}
