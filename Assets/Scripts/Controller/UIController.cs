using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    [Header("Game Info")]
    [SerializeField] Slider healthSlider;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Slider expSlider;
    [SerializeField] TextMeshProUGUI expText;
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] public Slider bossHpSlider;
    [Header("Warning")]
    public TextMeshProUGUI rageCount;
    public TextMeshProUGUI bossCount;


    [Header("Panels")]
    [SerializeField] public GameObject gameOverPanel;
    [SerializeField] public GameObject pauseMenuPanel;
    [SerializeField] public GameObject levelUpPanel;
    [SerializeField] public GameObject winPanel;


    public LevelUpButton[] levelUpButtons;

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
      
    public void UpdateHealth()
    {
        if (PlayerController.instance != null)
        {
            healthSlider.maxValue = PlayerController.instance.maxHp;
            healthSlider.value = PlayerController.instance.currentHp;
            healthText.text = healthSlider.value + "/" + healthSlider.maxValue;
        }
    }

    //Ability to level up is not implemented yet
    public void UpdateExp()
    {
        if (PlayerController.instance != null)
        {
            expSlider.maxValue = PlayerController.instance.playerLevels[PlayerController.instance.currentLevel - 1];
            expSlider.value = PlayerController.instance.exp;
            expText.text = expSlider.value + "/" + expSlider.maxValue;
            if(PlayerController.instance.currentLevel == PlayerController.instance.maxLevel)
            {
                expText.text = "Max Level";
                expSlider.value = expSlider.maxValue;

            }
        }
    }

    public void UpdateTimer(float remainingTime)
    {
        float min = Mathf.FloorToInt(remainingTime / 60);
        float sec = Mathf.FloorToInt(remainingTime % 60);
        timer.text = string.Format("{0:00}:{1:00}", min, sec);
    }
    public void UpdateBossHp(BossSpawn boss)
    {
        bossHpSlider.gameObject.SetActive(true);
        bossHpSlider.maxValue = boss.health;
        bossHpSlider.value = boss.currentHealth;
    }

    public void LevelUpOpen()
    {
        levelUpPanel.SetActive(true);
        Time.timeScale = 0f;

        List<UpgradeOption> options = UpgradeDatabase.instance.GetRandomUpgrade(2, 2);
        for (int i = 0; i < levelUpButtons.Length; i++) 
        {
            levelUpButtons[i].SetUp(options[i]);
        }
    }
    public void LevelUpClose()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }


}
