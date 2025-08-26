using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
    }

}
