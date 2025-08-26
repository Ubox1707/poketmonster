using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName ="Upgrade/New Support Item")]
public class SupportItem : UpgradeOption
{
    public enum SupportType { Heal, MaxHp, Speed, Invicible}
    public SupportType supportType;
    public string supportDescription;
    public int value;
    public override string description => supportDescription;
    public override bool CanBeCalled(PlayerController player)
    {
        return true;
    }
    public override void UpgradeApply(PlayerController player)
    {
        switch (supportType)
        {
            case SupportType.Heal:
                player.currentHp+= value;
                break;
            case SupportType.MaxHp:
                player.maxHp += value;
                break;
            case SupportType.Speed:
                player.speed += player.speed*((float)value/100f);
                break;
            case SupportType.Invicible:
                player.iframeDuration += value;
                break;
        }
    }
}
