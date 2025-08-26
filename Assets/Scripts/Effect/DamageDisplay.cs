using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDisplay : MonoBehaviour
{
    public static DamageDisplay instance;
    [SerializeField] DamageNumber prefab;
    [SerializeField] DamageNumber playerHit;



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

    public void FloatDamage(int value, Vector3 location)
    {
        DamageNumber damageNumber = Instantiate(prefab, location, transform.rotation, transform);
        damageNumber.SetDamage(value);
    }
    public void FloatPlayerDamage(int value, Vector3 location)
    {
        DamageNumber damageNumber = Instantiate(playerHit, location, transform.rotation, transform);
        damageNumber.SetDamage(value);
    }
}
