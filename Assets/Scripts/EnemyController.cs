using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

enum MovementState
{
    IDLE,
    MOVING,
    FLEEING,
    ATTACK,
    ROAM,
}

public class EnemyController : MonoBehaviour
{
    [SerializeField] private CharacterMovement characterMovement;

    [SerializeField] private CharacterStats stats;

    [SerializeField] private float attackRange = 1.0f;

    [SerializeField] private float attackCooldown = 1.0f;

    [SerializeField] private float fleeChance = 10.0f;
    [SerializeField] private float fleeCooldown = 1.0f;
    [SerializeField] private float idleChance = 10.0f;
    [SerializeField] private float idleCooldown = 3.0f;
    [SerializeField] private float moveCooldown = 1.0f;

    public WeaponController weaponController;

    [SerializeField] GameObject attackHitbox;

    public Animator animator;

    private float lastStateChange;

    public GameObject mushroomPrefab;
    public GameObject healthPrefab;

    public int[] dropChances;


    private MovementState movementState = MovementState.IDLE;
    
    private bool isPunching = false;
    private float horizontalInput;
    private float verticalInput;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        horizontalInput = 0;
        verticalInput = 0;

        player = GameObject.Find("Player");

        StartCoroutine(UpdateMovementState());
    }
	
    public void TakeDamage(int damage)
    {
        stats.removeHealth(damage);
    }

    private void FixedUpdate()
    {
         if (!player)
        {
            StopAllCoroutines();
            movementState = MovementState.ROAM;
        } 

        if (isPunching) return;
    
        CalculateMovement();

        bool shouldStop = movementState == MovementState.ATTACK || movementState == MovementState.IDLE;
        characterMovement.Move(horizontalInput, verticalInput, shouldStop);

        if(movementState == MovementState.ATTACK)
        {
            animator.SetBool("attack", true);
            isPunching = true;
        }
    }

    public void OnPunchEnded()
    {
        animator.SetBool("attack", false);
        isPunching = false;

        if(attackHitbox != null)
        {
            AttackScript attackScript = attackHitbox.GetComponent<AttackScript>();
            if (attackScript != null) {
                attackScript.OnPunchEnded();
                attackHitbox.SetActive(false);
            }
        }
    }

    public void OnPunched()
    {
        if(weaponController)
        {
            weaponController.ammo = 1;
            if(weaponController.CanShoot())
            {
                weaponController.Shoot(!characterMovement.facingRight);
            }
        }
        else
        {
            if(attackHitbox != null)
            {
                attackHitbox.SetActive(true);
            }
        }
    }

    private void CalculateMovement()
    {
        if(!player)
        {
            movementState = MovementState.ROAM;
        }

        if(movementState != MovementState.IDLE && movementState != MovementState.ATTACK)
        {
            float xDiff=0;
            float yDiff=0;
            if(movementState == MovementState.ROAM)
            {
                xDiff = UnityEngine.Random.Range(-1.0f, 1.0f);
                yDiff = UnityEngine.Random.Range(-1.0f, 1.0f);
            }
            else
            {
                Vector3 playerPosition = player.transform.position;
                Vector3 enemyPosition = transform.position;
                xDiff = playerPosition.x - enemyPosition.x;
                yDiff = playerPosition.y - enemyPosition.y;
            }

            if(Mathf.Approximately(xDiff, 0.0f))
            {
                horizontalInput = 0;
            }
            else if (xDiff > 0)
            {
                horizontalInput = 1;
            }
            else
            {
                horizontalInput = -1;
            }

            if(Mathf.Approximately(yDiff, 0.0f))
            {
                verticalInput = 0;
            }
            else if (yDiff > 0)
            {
                verticalInput = 1;
            }
            else
            {
                verticalInput = -1;
            }

            if(movementState == MovementState.FLEEING)
            {
                horizontalInput *= -0.5f;
                verticalInput *= -0.5f;
            }
            animator.SetFloat("speed", 1);
        }
        else
        {
            horizontalInput = 0;
            verticalInput = 0;
            animator.SetFloat("speed", 0);
        }

    }



    IEnumerator UpdateMovementState()
    {
        while(true)
        {

            if(!player)
            {
                movementState = MovementState.ROAM;
            }
            else
            {
                Vector3 playerPosition = player.transform.position;
                Vector3 enemyPosition = transform.position;

                if(Vector2.Distance(playerPosition, enemyPosition) < attackRange)
                {
                    movementState = MovementState.ATTACK;
                    yield return new WaitForSeconds(attackCooldown);
                }
                else if(Time.time > lastStateChange)
                {
                    int random = UnityEngine.Random.Range(0, 100);
                    if(random < fleeChance)
                    {
                        movementState = MovementState.FLEEING;
                        lastStateChange = Time.time + fleeCooldown;
                    }
                    else if(random < idleChance + fleeChance)
                    {
                        movementState = MovementState.IDLE;
                        lastStateChange = Time.time + idleCooldown;
                    }
                    else
                    {
                        movementState = MovementState.MOVING;
                        lastStateChange = Time.time + moveCooldown;
                    }

                    yield return new WaitForSeconds(0.1f);
                }
                else
                {
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
    }

    public void OnDeath()
    {
        // spawn a random item from the dropChances array
        int random = UnityEngine.Random.Range(0, 100);
        int sum = 0;
        for(int i = 0; i < dropChances.Length; i++)
        {
            sum += dropChances[i];
            if (random < sum)
            {
                GameObject spawned = Instantiate(mushroomPrefab, transform.position, Quaternion.identity);
                MushDrops spawnedMush = spawned.GetComponent<MushDrops>();
                spawnedMush.setMushIndex(i);
                break;
            }         
        }

        ScoreManager scoreManager = player.GetComponent<PlayerController>().scoreManager;
        scoreManager.IncreaseScore(stats.scoreAmount);
        Destroy(gameObject);

        SpawnController spawnController = GameObject.Find("SpawnController").GetComponent<SpawnController>();
        spawnController.KilledEnemy();
    }
}