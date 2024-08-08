using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerAttack : MonoBehaviour
{
    public WeaponController weaponController;
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy")) {
                other.gameObject.GetComponent<EnemyController>().TakeDamage(weaponController.dmg);
            }
        }
        
}
