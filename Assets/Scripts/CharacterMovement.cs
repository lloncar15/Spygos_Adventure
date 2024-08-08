using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private CharacterStats stats;
    [SerializeField] public Transform walkableGroundTransform;
    private float minX, maxX, minY, maxY;

    private Vector3 velocity = Vector3.zero;

    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
    
    private Rigidbody2D rb;

    public bool facingRight = true;
    private bool canMove = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Bounds spriteBounds = GetComponent<SpriteRenderer>().bounds;

        Bounds groundBounds = walkableGroundTransform.GetComponent<SpriteRenderer>().bounds;

        maxX = groundBounds.max.x - spriteBounds.size.x / 2f;
        minX = groundBounds.min.x + spriteBounds.size.x / 2f;
        maxY = groundBounds.max.y + spriteBounds.size.y / 4f;
        minY = groundBounds.min.y + spriteBounds.size.y / 2f;
    }

    public void Move(float hMove, float vMove, bool stop)
    {
        if (!canMove) {
            return;
        }
        

        if(stop) {
            rb.velocity = Vector3.zero;
        } else {
            // move the character by finding the target velocity
            Vector3 targetVelociy = new Vector2(hMove * stats.getSpeedX(), vMove * stats.getSpeedY());
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelociy, ref velocity, movementSmoothing);
        }

        // don't go out of bounds
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY),
            transform.position.z
        );

        // rotate if we're facing the wrong way
        if (hMove > 0 && !facingRight) {
            Flip();
        }
        else if (hMove < 0 && facingRight) {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
