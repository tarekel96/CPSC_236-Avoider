﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;

    // vars for spawning enemies
    public float timerMin = 5f;
    public float timerMax = 25f;

    // player ~ user
    public GameObject player;

    // vars for enemy bullets
    private int numOfBullets = 0;
    private int MAX_NUM_OF_BULLETS = 3;
    public bool canFireBullets = true;
    private float timerBullet;
    private float maxTimerBullet;
    public GameObject bullet;

    // vars for enemy movement
    private Vector2 relativePosition;
    private float moveSpeed = 1.75f;
    private Vector2 enemyMovement;
    private float enemyDetectionRadius = 5f;
    // vars for rand enemy movement
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        // calculations for enemy movement
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();

        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveSpeed, moveSpeed);

        timerBullet = 0;
        maxTimerBullet = Random.Range(timerMin, timerMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // relative poistion of the target (player) based upon the current position
            relativePosition = new Vector2(
                player.GetComponent<Rigidbody2D>().position.x - gameObject.transform.position.x,
                player.GetComponent<Rigidbody2D>().position.y - gameObject.transform.position.y);
        }

        if(relativePosition.x < enemyDetectionRadius && relativePosition.y < enemyDetectionRadius)
        {
            canFireBullets = true;
        }
        else
        {
            canFireBullets = false;
            //if the changeTime was reached, calculate a new movement vector
            if (Time.time - latestDirectionChangeTime > directionChangeTime)
            {
                latestDirectionChangeTime = Time.time;
                calcuateNewMovementVector();
            }
        }

        if (canFireBullets && (numOfBullets < MAX_NUM_OF_BULLETS))
        {
            StartCoroutine("FireBullet");
        }
        // destroys enemy once it gets out of camera viewport
        if (Camera.main.WorldToViewportPoint(transform.position).y < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(player != null && canFireBullets)
        {
            MoveTowardsPlayer();
        }
        else if(player != null && !canFireBullets)
        {
            RandomMove();
        }
    }

    void SpawnBullet()
    {
        numOfBullets++;
        Vector3 spawnPoint = transform.position;
        spawnPoint.y -= (bullet.GetComponent<Renderer>().bounds.size.y / 2) + (GetComponent<Renderer>().bounds.size.y / 2);
        GameObject.Instantiate(bullet, spawnPoint, transform.rotation);
    }

    IEnumerator FireBullet()
    {
        if (timerBullet >= maxTimerBullet)
        {
            // spawn an enemy
            SpawnBullet();
            timerBullet = 0;
            maxTimerBullet = Random.Range(timerMin, timerMax);
        }

        timerBullet += 0.1f;
        // yield for CoRoutine
        yield return new WaitForSeconds(0.5f);
    }

    void MoveTowardsPlayer()
    {
        if (moveSpeed * Time.deltaTime >= Mathf.Abs(relativePosition.x))
        {
            enemyMovement.x = relativePosition.x;
        }
        else
        {
            enemyMovement.x = moveSpeed * Mathf.Sign(relativePosition.x);
        }
        if (moveSpeed * Time.deltaTime >= Mathf.Abs(relativePosition.y))
        {
            enemyMovement.y = relativePosition.y;
        }
        else
        {
            enemyMovement.y = moveSpeed * Mathf.Sign(relativePosition.y);
        }

        rb.velocity = enemyMovement;
    }

    void calcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * moveSpeed;
    }

    void RandomMove()
    {
        //move enemy: 
        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
        transform.position.y + (movementPerSecond.y * Time.deltaTime));
    }
}