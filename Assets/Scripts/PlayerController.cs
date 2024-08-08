using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterMovement characterMovement;
    public CharacterStats characterStats;
    public PlaySound playSound;
    private Rigidbody2D rb;

    public Animator animator;

    private float horizontalInput, verticalInput;

    public GameObject attackHitbox;

    private bool isPunching = false;
    private bool specialAttack1;
    private bool specialAttack2;
    private bool specialAttack3;

    public WeaponController flowerWeapon;

    public WeaponController saxWeapon;

    public WeaponController lolliWeapon;

    public ScoreManager scoreManager;

    public HealthBar healthBar;

    public GameOverScreen gameOverScreen;

    public bool isDead = false;

    public SuperAttack superAttack;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth();
        GameController.Instance.SetBackgroundSprite();
    }


    private void Update()
    {
        if (isDead) return;

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        float maxInput = Mathf.Max(Mathf.Abs(horizontalInput), Mathf.Abs(verticalInput));
        animator.SetFloat("speed", maxInput);

        if (Input.GetButtonDown("Punch") && !isPunching)
        {
            if (!GameController.Instance.hasUsedSuperAttack && characterStats.currentHealth <= 20)
            {
                GameController.Instance.hasUsedSuperAttack = true;
                superAttack.superAttack();
            }
            else{
                isPunching = true;
            animator.SetTrigger("punch");
            }
        }

        if (Input.GetButtonDown("Fire1") && flowerWeapon.CanShoot()) {
            flowerWeapon.ShootFlower();
            animator.SetTrigger("flower");
            isPunching = true;
        }

        if (Input.GetButtonDown("Fire2") && saxWeapon.CanShoot()) {
            saxWeapon.Shoot(!characterMovement.facingRight);
            animator.SetTrigger("sax");
        }

        if (Input.GetButtonDown("Fire3") && lolliWeapon.CanShoot()) {
            animator.SetTrigger("lolli");
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;
        if (isPunching) {
            rb.velocity = Vector2.zero;
            return;
        }

        characterMovement.Move(horizontalInput, verticalInput, false);
    }

    public void OnPunchEnded()
    {
        animator.SetTrigger("punchEnded");
        isPunching = false;
        attackHitbox.GetComponent<AttackScript>().OnPunchEnded();
        attackHitbox.SetActive(false);
    }

    public void OnPunched()
    {
        attackHitbox.SetActive(true);
    }

    public void IncreaseHealth(int health)
    {
        characterStats.addHealth(health);
        GameController.Instance.health = characterStats.currentHealth;
        healthBar.SetHealth();
    }

    public void TakeDamage(int damage)
    {
        characterStats.removeHealth(damage);
        GameController.Instance.health = characterStats.currentHealth;
        healthBar.SetHealth();
    }

    public void OnSaxended()
    {
        animator.SetTrigger("saxEnded");
        isPunching = false;
    }

    public void OnFlowerEnded()
    {
        animator.SetTrigger("flowerEnded");
        isPunching = false;
    }

    public void OnLolliEnded()
    {
        animator.SetTrigger("lolliEnded");
        isPunching = false;
    }

    public void OnLolliFired()
    {
        lolliWeapon.Shoot(!characterMovement.facingRight);
    }

    public void onDeath()
    {
        animator.SetTrigger("death");
        isDead = true;
        rb.velocity = Vector2.zero;
    }
}
