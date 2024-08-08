using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LolliBulletController : BulletController
{
    public Sprite[] sprites;

    protected override void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        base.Start();

        rb.velocity = new Vector3(speed * (isFlipped ? -1 : 1), 0, 0);
    }

    void Update()
    {
        rb.rotation -= 2f;
        if(rb.rotation < -360f)
        {
            rb.rotation += 360f;
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (isPlayer)
        {
            if (other.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyController>().TakeDamage(dmg);
            }
        }
    }
}
