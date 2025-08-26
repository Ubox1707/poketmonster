using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI damageText;
    float floatSpeed;
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        floatSpeed = Random.Range(0.1f, 1f);
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }

    public void SetDamage(int damage)
    {
        damageText.text = damage.ToString();
    }
}
