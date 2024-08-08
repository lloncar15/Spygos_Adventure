using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    public Image[] introSprites;

    private bool isAnimationInProgress = false;

    private int currentIntroSpriteIndex = 0;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    private void StartImageAnimation()
    {
        isAnimationInProgress = true;
        ++currentIntroSpriteIndex;
        // do scaling in animation of the current image with tweening
        EndImageAnimation();
    }

    private void EndImageAnimation()
    {
        isAnimationInProgress = false;
    }
}
