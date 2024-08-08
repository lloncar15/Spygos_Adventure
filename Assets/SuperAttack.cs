using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttack : MonoBehaviour
{
    public Animator mainCamera;

    public int superAttackDamage = 100;

    public PlaySound s;

    public void CameraShake()
    {
        mainCamera.SetTrigger("shakeEnter");
    }

    public void superAttack()
    {
        GetComponent<Animator>().SetTrigger("superAttack");
    }

    public void OnSuperAttackHit()
    {
        if(GameObject.FindWithTag("Player").GetComponent<PlayerController>().isDead){
            return;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        s.onEarth();

        // Now 'enemies' array contains all game objects with the "Enemy" tag
        foreach (GameObject enemy in enemies)
        {
            CharacterStats enemyStats = enemy.GetComponent<CharacterStats>();
            if (enemyStats != null) {
                enemyStats.removeHealth(superAttackDamage);
            }
        }
    }

}
