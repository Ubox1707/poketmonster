using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpButton : MonoBehaviour
{
    [Header("Level Up")]
    [SerializeField] public TextMeshProUGUI itemName;
    [SerializeField] public Image itemIcon;
    [SerializeField] public TextMeshProUGUI itemDescription;

    private UpgradeOption assignedUpgrade;
    //Weapon assignedWeapon;

    public void SetUp(UpgradeOption upgrade)
    {
        assignedUpgrade = upgrade;
        itemName.text = upgrade.upgradeName;
        itemIcon.sprite = upgrade.icon;
        itemDescription.text = upgrade.description;
    } 
    
    public void SelectItem()
    {
        assignedUpgrade.UpgradeApply(PlayerController.instance);
        UIController.instance.LevelUpClose();
        AudioManager.instance.PlaySound(AudioManager.instance.selectUpgrade);
    }

    //public void ActiveButton(Weapon weapon)
    //{
    //    itemName.text = weapon.name;
    //    itemIcon.sprite = weapon.weaponIcon;
    //    itemDescription.text = weapon.weaponStats[weapon.weaponLevel].description;
    //    assignedWeapon = weapon;
    //}

    //public void SelectItem()
    //{
    //    assignedWeapon.LevelUp();
    //    UIController.instance.LevelUpClose();
    //}
}
