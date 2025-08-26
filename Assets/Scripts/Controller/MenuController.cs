using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

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
    public void PlayGame()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.buttonClick);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void QuitGame()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.buttonClick);

        Application.Quit();

    }
}
