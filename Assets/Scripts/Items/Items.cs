using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Items : MonoBehaviour
{
    public enum ItemType { itemHP, itemEXP, maxHp, itemSpeed}
    public ItemType itemType;
    bool isHPItem = false;
    bool isEXPItem = false;
    bool isMaxHpItem = false;
    bool isSPeedItem = false;
    [SerializeField] int point;
    private void Start()
    {
        switch (itemType)
        {
            case ItemType.itemHP:
                isHPItem = true;
                break;
            case ItemType.itemEXP:
                isEXPItem = true;
                break;
            case ItemType.maxHp:
                isMaxHpItem = true;
                break;
            case ItemType.itemSpeed:
                isSPeedItem = true;
                break;
        }
        Destroy(gameObject, 7f);
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isHPItem)
            {
                PlayerController.instance.currentHp += point;
            }
            else if (isEXPItem)
            {
                PlayerController.instance.exp += point;
            }
            else if (isMaxHpItem)
            {
                PlayerController.instance.maxHp += point;
            }
            else if (isSPeedItem)
            {
                PlayerController.instance.speed += PlayerController.instance.speed*((float)point/100f);
            }
            AudioManager.instance.PlaySound(AudioManager.instance.selectUpgrade);
            Destroy(gameObject);
        }
        else if(collision.CompareTag("Weapon") || collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
        {
            return;
        }

    }

}
