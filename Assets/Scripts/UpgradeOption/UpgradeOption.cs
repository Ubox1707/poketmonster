using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeOption : ScriptableObject
{
    public string upgradeName;
    public Sprite icon;
    public abstract string description { get; }
    public abstract void UpgradeApply(PlayerController player);
    public virtual bool CanBeCalled(PlayerController player)
    {
        return true;
    }
}
