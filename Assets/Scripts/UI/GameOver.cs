using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Public variables
    public UnityEngine.Audio.AudioMixer audioMixer;
    public GameObject gameOverMenu;

    private bool menuTrigger = false;

    public void triggerGameOverMenu()
    {
        StartCoroutine(triggerGameOver());
    }

    IEnumerator triggerGameOver()
    {
        yield return new WaitForSeconds(2.0f);
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
    }



    public void RetryLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
 

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
