using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverScreen;

    public void OnGameOver()
    {
        gameOverScreen.SetActive(true);

        GameController.Instance.SaveHighScore();
    }

    public void OnGameOverClicked()
    {
        Time.timeScale = 1;
        GameController.Instance.ResetStats();
        SceneManager.LoadScene("MainMenuScene");
    }
}
