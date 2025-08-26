using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossItem : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Transform target;
    private void Start()
    {
        target = PlayerController.instance.transform;
    }

    private void Update()
    {
        if(target ==null)
        {
            return;
        }
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);
    }
}
