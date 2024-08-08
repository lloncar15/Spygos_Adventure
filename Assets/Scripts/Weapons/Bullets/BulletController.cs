using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int dmg = 0;
    public bool isPlayer = false;
    public float speed = 1f;

    public float startX, startY;

    public bool isFlipped = false;

    private float destroyXBoundary = 100f;

    public Rigidbody2D rb;

    protected virtual void Start()
    {
        startX = transform.position.x;
        startY = transform.position.y;
        destroyXBoundary = startX + destroyXBoundary;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float xPos = transform.position.x;
        // Check if the bullet is beyond the specified boundaries and destroy it
        if (Mathf.Abs(xPos) > destroyXBoundary)
        {
            Destroy(gameObject);
        }
    }

    virtual public void OnTriggerEnter2D(Collider2D other)
    {
        if (isPlayer)
        {
            if (other.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyController>().TakeDamage(dmg);
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerController>().TakeDamage(dmg);
                Destroy(gameObject);
            }
        }
    }
}
