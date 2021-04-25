using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;

    private float timerBullet;
    private float maxTimerBullet;
    public GameObject bullet;

    public float timerMin = 5f;
    public float timerMax = 25f;
    public bool canFireBullets = true;

    public Rigidbody2D player;
    private Vector2 relativePosition;
    private float moveSpeed = 1.75f;
    private Vector2 enemyMovement;
    private int numOfBullets = 0;
    private int MAX_NUM_OF_BULLETS = 3;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveSpeed, moveSpeed);

        timerBullet = 0;
        maxTimerBullet = Random.Range(timerMin, timerMax);
    }

    // Update is called once per frame
    void Update()
    {
        // relative poistion of the target (player) based upon the current position
        relativePosition = new Vector2(
            player.position.x - gameObject.transform.position.x,
            player.position.y - gameObject.transform.position.y);

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
        Move();
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

    void Move()
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
}