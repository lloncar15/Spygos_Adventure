using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNoteBullet : BulletController
{
    public float amplitude = 1f;
    public float frequency = 1f;
    public Sprite[] sprites;
    private float startTime;
    private bool useFirstWave;

    protected override void Start()
    {
        base.Start();

        startTime = Time.time;
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];

        // Initialize useFirstWave randomly
        useFirstWave = Random.Range(0, 2) == 0; // 50% chance to be true
    }

    void FixedUpdate()
    {
        float time = Time.time - startTime;
        float xPos = startX + time * speed * (isFlipped ? -1 : 1);

        float yPos;

        if (useFirstWave)
        {
            yPos = startY + Mathf.Sin(time * frequency) * amplitude;
        }
        else
        {
            yPos = startY + Mathf.Sin(-time * frequency + 3 / 4 * Mathf.PI) * amplitude;
        }

        transform.position = new Vector3(xPos, yPos, 0);
    }
}