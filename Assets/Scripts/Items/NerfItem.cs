using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NerfItem : MonoBehaviour
{
    public enum ItemType { itemHP, itemEXP }
    public ItemType itemType;
    bool isHpItem = false;
    bool isExpItem = false;
    [SerializeField] int point;

    private void Start()
    {
        switch (itemType)
        {
            case ItemType.itemHP:
                isHpItem = true;
                break;
            case ItemType.itemEXP:
                isExpItem = true;
                break;
        }
        StartCoroutine(WaitToDestroy());

    }
    private IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(7f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isHpItem)
            {
                PlayerController.instance.currentHp -= point;
            }
            if (isExpItem)
            {
                PlayerController.instance.exp -= point;
            }

            AudioManager.instance.PlaySound(AudioManager.instance.debugItem);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Weapon") || collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
        {
            return;
        }
    }
}
