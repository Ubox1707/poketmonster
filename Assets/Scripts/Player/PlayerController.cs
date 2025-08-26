using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    [Header("Player Stats")]
    public float speed;
    public int maxHp;
    public int currentHp;
    public Transform firePos;
    public int exp;
    public int maxLevel;
    public int currentLevel;
    public List<int> playerLevels;
    public FloatingJoystick joystick;

    [Header("Iframes")]
    [SerializeField] public float iframeDuration;
    [SerializeField] float flashTime;

    [Header("Player Dash")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;
    [SerializeField] float dashCooldown;
    [SerializeField] Image dashImage;
    [SerializeField] ParticleSystem dust;
    bool isDashing;
    bool canDash;
    bool gameOver = false;

    public Vector2 moveDirection { get; private set; }
    public Vector2 previousDir { get; private set; } = Vector2.right;

    SpriteRenderer sprite;
    Rigidbody2D rb;
    Animator anim;
    float horizontalInput;
    float verticalInput;
    int originslLayer;
    //public Weapon activeWeapon;
    public Weapon[] startWeapon;
    public List<Weapon> weapons;

    public bool isWin = false;


    bool isInvicible;
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
        currentHp = maxHp;

    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        originslLayer = gameObject.layer;
        //UIController.instance.UpdateHealth();
        dashImage.fillAmount = 0f;
        canDash = true;
        gameOver = false;
        UIController.instance.UpdateExp();

        for (int i = playerLevels.Count; i < maxLevel; i++)
        {
            playerLevels.Add(Mathf.CeilToInt(playerLevels[playerLevels.Count - 1] * 1.2f + 70));
        }
        //-------------For starting weapon is default weapon-----------------
        //Weapon startingWeapon = GetComponentInChildren<Weapon>();
        //if (startingWeapon != null)
        //{
        //    weapons.Add(startingWeapon);
        //}


        //-------------For random weapon is default weapon-----------------


        //Use the original prefab to instantiate the weapon so it cause error because the level up system is affect the original prefab
        //for (int i =0; i<startWeapon.Length; i++)
        //{
        //    startingWeapon = startWeapon[Random.Range(0, startWeapon.Length)];
        //}
        //if (startingWeapon != null)
        //{
        //    weapons.Add(startingWeapon);
        //    GameObject randWeapon = Instantiate(startingWeapon.gameObject, firePos.position, Quaternion.identity, transform);
        //}


        //Not using the original prefab to instantiate the weapon so it doesn't cause error
        if (startWeapon.Length > 0)
        {
            Weapon originPrefab = startWeapon[Random.Range(0, startWeapon.Length)];
            GameObject randWeapon = Instantiate(originPrefab.gameObject, firePos.position, Quaternion.identity, transform);
            Weapon objPrefab = randWeapon.GetComponent<Weapon>();
            objPrefab.weaponLevel = 0;
            weapons.Add(objPrefab);
        }
    }

    private void Update()
    {
        if (isDashing) return;
        //horizontalInput = Input.GetAxis("Horizontal");
        //verticalInput = Input.GetAxis("Vertical");
        horizontalInput = joystick.Horizontal;
        verticalInput = joystick.Vertical;

        anim.SetFloat("XVelocity", horizontalInput);
        anim.SetFloat("YVelocity", verticalInput);

        if (horizontalInput != 0 || verticalInput != 0)
        {
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }

        if (currentHp <= 0)
        {
            PlayerDeath();
        }
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        moveDirection = new Vector2(joystick.Horizontal, joystick.Vertical).normalized;
        if(moveDirection != Vector2.zero)
        {
            previousDir = moveDirection;
        }
        Dashing();
        UIController.instance.UpdateHealth();
        UIController.instance.UpdateExp();

    }

    private void FixedUpdate()
    {
        if (isDashing) return;
        rb.velocity = new Vector3(horizontalInput * speed, verticalInput * speed);

    }
   
    public void TakeDamage(int damage)
    {
        if (!isInvicible)
        {
            AudioManager.instance.PlaySound(AudioManager.instance.hurt);
            StartCoroutine(Invicible());
            currentHp -= damage;
            currentHp = Mathf.Clamp(currentHp, 0, maxHp);
            DamageDisplay.instance.FloatPlayerDamage(damage, transform.position);

        }

    }

    public void PlayerDeath()
    {
        gameObject.SetActive(false);
        gameOver = true;
        GameManager.instance.ShowGameOverPanel();
        AudioManager.instance.PlaySound(AudioManager.instance.gameOver);
    }
    public void PlayerWin()
    {
        isWin = true;
        GameManager.instance.ShowWinPanel();
    }

    IEnumerator Invicible()
    {
        isInvicible = true;
        for (int i = 0; i < flashTime; i++)
        {
            sprite.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(iframeDuration / (flashTime * 2));
            sprite.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.1f);
        }
        isInvicible = false;

    }

    public void GetExp(int expPlus)
    {
        exp += expPlus;
        UIController.instance.UpdateExp();
        if (exp >= playerLevels[currentLevel - 1] && !gameOver)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        if(currentLevel >= maxLevel)
        {
            exp = playerLevels[maxLevel - 1];
            return;
        }
        exp -= playerLevels[currentLevel - 1];
        currentLevel++;
        if (currentLevel >= maxLevel)
        {
            exp = playerLevels[maxLevel - 1];
        }
        UIController.instance.UpdateExp();
        UIController.instance.LevelUpOpen();
        

    }
    //For keyboard
    public void Dashing()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());

        }
    }
    public void DashButton()
    {
        if (canDash)
        {
            StartCoroutine(Dash());
        }
    }
    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        isInvicible = true;
        gameObject.layer = LayerMask.NameToLayer("PlayerDash");

        dust.Clear();
        dust.transform.position = transform.position;
        dust.Play();
        AudioManager.instance.PlaySound(AudioManager.instance.dash);

        rb.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);
        dashImage.fillAmount = 1f;

        yield return new WaitForSeconds(dashDuration);
        gameObject.layer = originslLayer;
        isDashing = false;
        dust.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        isInvicible = false;

        StartCoroutine(DashCooldown());
        //yield return new WaitForSeconds(dashCooldown);
        //canDash = true;
        //isInvicible = false;
    }
    IEnumerator DashCooldown()
    {
        float time = 0f;
        while (time < dashCooldown)
        {
            time += Time.deltaTime;
            dashImage.fillAmount = Mathf.Lerp(1f, 0f, time / dashCooldown);
            yield return null;
        }
        dashImage.fillAmount = 0f;
        canDash = true;
    }
}
