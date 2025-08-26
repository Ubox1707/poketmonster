using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    bool gameActive;

    public float remainingTime;
    [SerializeField] float rageDuration;
    [SerializeField] float limitTime = 300f;

    [Header("Power Up")]
    [SerializeField] float fadingInterval = 1.5f;

    float fadingTime;
    bool fadingIn = false;
    public bool textFadeIn = false;

    float rageTimer;
    int rageCount;
    
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

    private void Start()
    {
        gameActive = true;

        UIController.instance.rageCount.CrossFadeAlpha(0, 0.0f, false);
        UIController.instance.bossCount.CrossFadeAlpha(0, 0.0f, false);

        fadingTime = 0f;
        fadingIn = false;
        textFadeIn = false;
    }
    private void Update()
    {
        rageTimer += Time.deltaTime;
        remainingTime += Time.deltaTime;
        UIController.instance.UpdateTimer(remainingTime);
        if (rageTimer >= rageDuration)
        {
            rageTimer = 0f;
            EnemyPowerUp();

        }
        if (gameActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                PauseMenu();
            }
        }
        FadeText();
        
        EndGame();

    }
    
    void FadeText()
    {
        if (fadingIn)
        {

            FadeInIn(UIController.instance.rageCount);
        }
        else if (UIController.instance.rageCount.color.a != 0)
        {
            UIController.instance.rageCount.CrossFadeAlpha(0, 0.5f, false);

        }
        if (textFadeIn)
        {

            FadeInIn(UIController.instance.bossCount);
        }
        else if (UIController.instance.bossCount.color.a != 0)
        {
            UIController.instance.bossCount.CrossFadeAlpha(0, 0.5f, false);

        }
    }
    public void FadeInIn(TextMeshProUGUI fadeText)
    {
        fadeText.CrossFadeAlpha(1, 0.5f, false);
        fadingTime += Time.deltaTime;
        if (fadeText.color.a == 1 && fadingTime > fadingInterval)
        {
            fadingIn = false;
            textFadeIn = false;
            fadingTime = 0f;
        }
    }
    void EndGame()
    {
        if (remainingTime >= limitTime)
        {
            ShowWinPanel();
        }
    }
   
    public void PauseMenu()
    {
        if (!UIController.instance.pauseMenuPanel.activeSelf && !UIController.instance.gameOverPanel.activeSelf)
        {
            UIController.instance.pauseMenuPanel.SetActive(true);
            Time.timeScale = 0f;
            AudioManager.instance.PlaySound(AudioManager.instance.pause);
        }
        else
        {
            UIController.instance.pauseMenuPanel.SetActive(false);
            Time.timeScale = 1f;
            AudioManager.instance.PlaySound(AudioManager.instance.unPause);

        }
    }
    public void ResumeGame()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.buttonClick);
        UIController.instance.pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ShowGameOverPanel()
    {
        gameActive = false;
        StartCoroutine(WaitDeath());
    }
    public void ShowWinPanel()
    {
        gameActive = false;
        StartCoroutine(WaitWin());

    }
    IEnumerator WaitDeath()
    {
        yield return new WaitForSeconds(1.5f);
        UIController.instance.gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

    }

    IEnumerator WaitWin()
    {

        yield return new WaitForSeconds(1f);
        AudioManager.instance.PlaySound(AudioManager.instance.winSound);
        UIController.instance.winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.buttonClick);
        UIController.instance.gameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void Menu()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.buttonClick);

        UIController.instance.gameOverPanel.SetActive(false);
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    void EnemyPowerUp()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.warning);
        fadingIn = true;
        rageCount++;
        UIController.instance.rageCount.text = "Rage " + rageCount.ToString();

        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            if (enemy != null || !enemy.gameObject.activeInHierarchy)
            {
                if (!(enemy is BossSpawn))
                {
                    enemy.PowerUp();
                }
            }
        }
    }
    public int GetPowerUpCount()
    {
        return rageCount;
    }
}
