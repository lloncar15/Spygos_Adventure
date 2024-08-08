using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public int _score = 0;
    public int _backgroundIndex = 0;

    public int _currentLevel = 0;

    public Sprite[] _backgroundSprites;

    public SpriteRenderer backgroundSprite;

    public int[] ammos;
    public int health;

    public Vector3[] enemies;

    public bool hasUsedSuperAttack = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            ammos[0] = 12;
            ammos[1] = 15;
            ammos[2] = 5;

            health = 100;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public void SaveHighScore()
    {
        if (_score > PlayerPrefs.GetInt("HighScore", 0)) {
            PlayerPrefs.SetInt("HighScore", _score);
        }
    }

    public void SetBackgroundSprite()
    {
        backgroundSprite.sprite = _backgroundSprites[_backgroundIndex];
        _backgroundIndex = (_backgroundIndex + 1) % _backgroundSprites.Length;
    }

    public void ResetStats()
    {
        ammos[0] = 12;
        ammos[1] = 15;
        ammos[2] = 5;

        health = 100;

        _backgroundIndex = 0;
        _score = 0;
        _currentLevel = 0;
    }
}
