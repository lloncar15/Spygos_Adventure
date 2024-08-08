using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public CharacterStats characterStats;

    public bool isPlayer = false;

    private bool hitDetected = false;

    private void Start()
    {
        characterStats = gameObject.GetComponentInParent<CharacterStats>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hitDetected) return;

        if (isPlayer) {
            if (other.CompareTag("Enemy")) {
                gameObject.SetActive(false);
                hitDetected = true;
                other.gameObject.GetComponent<EnemyController>().TakeDamage(characterStats.getDamage());
            }
        }
        else {
            if (other.CompareTag("Player")) {
                gameObject.SetActive(false);
                hitDetected = true;
                other.gameObject.GetComponent<PlayerController>().TakeDamage(characterStats.getDamage());
            }
        }
        
    }

    public void OnPunchEnded()
    {
        hitDetected = false;
    }
}
