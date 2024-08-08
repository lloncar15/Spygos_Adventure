using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushDrops : MonoBehaviour
{
    public int mushIndex = 0;

    public WeaponController[] weapons;

    public Sprite[] sprites;

    public Animator animator;

    public ScoreManager scoreManager;

    public PlayerController player;

    public int healthCollected;


    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        weapons[0] = player.flowerWeapon;
        weapons[1] = player.saxWeapon;
        weapons[2] = player.lolliWeapon;

        scoreManager = player.scoreManager;
    }

    public void setMushIndex(int index)
    {
        mushIndex = index;
        GetComponent<SpriteRenderer>().sprite = sprites[index];
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("death");

            if (mushIndex == 3) 
            {
                player.IncreaseHealth(healthCollected);
            }
            else 
            {
                weapons[mushIndex].ammo = weapons[mushIndex].ammo + 1;
                scoreManager.UpdateAmmo();
            }
        }
    }


    void OnDeath()
    {
        Destroy(gameObject);
    }

    void OnSpawn()
    {
        animator.SetTrigger("spawn");
    }
}