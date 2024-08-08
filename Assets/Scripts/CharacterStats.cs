using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] int damage;
    [SerializeField] float speedX;
    [SerializeField] float speedY;

    public int flowerDamage;

    public int scoreAmount = 1;

    public int currentHealth;

    void Start()
    {
        if (gameObject.CompareTag("Player")) {
            currentHealth = GameController.Instance.health;
        }
        else {
            currentHealth = maxHealth;
        }
    }

    public void  addHealth(int value)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + value);
    }

    public void removeHealth(int value)
    {
        currentHealth = Mathf.Max(0, currentHealth - value);
        if(currentHealth <= 0)
        {
            if(gameObject.CompareTag("Player"))
            {
                GameOverScreen gos = gameObject.GetComponent<GameOverScreen>();
                gos.OnGameOver();
                GameController.Instance._score = 0;
                gameObject.GetComponent<PlayerController>().onDeath();
            }
            else if(gameObject.CompareTag("Enemy"))
            {
                // trigger enemy controller OnDeath event
                EnemyController enemyController = GetComponent<EnemyController>();
                enemyController.OnDeath();
            }
        }
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public int getHealth()
    {
        return currentHealth;
    }

    public int getDamage()
    {
        return damage;
    }

    public float getSpeedX()
    {
        return speedX;
    }

    public float getSpeedY()
    {
        return speedY;
    }
}
