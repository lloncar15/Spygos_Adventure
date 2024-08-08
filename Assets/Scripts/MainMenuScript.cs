using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    public Animator animator;

    void Start()
    {
        highScoreText.text = $"Highscore: {PlayerPrefs.GetInt("HighScore", 0)}";
    }

    public void OnPlayClicked()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void OnQuickClicked()
    {
        Application.Quit();
    }

    public void OnTutorialOpened()
    {
        animator.SetTrigger("tuts");
    }

    public void OnTutorialClosed()
    {
        animator.SetTrigger("tutsBakc");
    }
}
