using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource pause;
    public AudioSource unPause;
    public AudioSource enemyDie;
    public AudioSource selectUpgrade;
    public AudioSource buttonClick;
    public AudioSource gameOver;
    public AudioSource shooting;
    public AudioSource weaponSpawn;
    public AudioSource weaponRespawn;
    public AudioSource skullSpawn;
    public AudioSource debugItem;
    public AudioSource winSound;
    public AudioSource bulletHit;
    public AudioSource skullHit;
    public AudioSource dash;
    public AudioSource warning;
    public AudioSource bossIntro;
    public AudioSource hurt;







    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlaySound(AudioSource sound)
    {
        sound.Stop();
        sound.Play();
    }
    public void PlayModifiedSound(AudioSource sound)
    {
        sound.pitch = Random.Range(0.7f, 1.5f);
        sound.Stop();
        sound.Play();
    }
}
