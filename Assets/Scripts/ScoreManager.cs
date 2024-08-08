using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public WeaponController[] weapons;
    public TextMeshProUGUI[] ammoTexts;
  
    public int currentScore = 0;


    void Start()
    {
        currentScore = GameController.Instance._score;
        UpdateScoreText();
        for (int i = 0; i < weapons.Length; i++)
        {
            ammoTexts[i].text = weapons[i].ammo.ToString();
        }
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + GameController.Instance._score.ToString();
    }

    // Public method to increase the score
    public void IncreaseScore(int amount)
    {
        currentScore += amount;
        GameController.Instance._score = currentScore;
        UpdateScoreText();
    }

    public void IncreaseAmmo(int amount, int index)
    {
        weapons[index].ammo += amount;
        ammoTexts[index].text = weapons[index].ammo.ToString();
    }

    public void UpdateAmmo()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            ammoTexts[i].text = weapons[i].ammo.ToString();
            GameController.Instance.ammos[i] = weapons[i].ammo;
        }
    }
}