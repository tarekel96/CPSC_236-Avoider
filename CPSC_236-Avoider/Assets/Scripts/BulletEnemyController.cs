using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject player;
    private Vector2 relativePosition;
    private float moveSpeed = 1.25f;
    private Vector2 enemyBulletMovement;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveSpeed, moveSpeed);
        
    }

    // Update is called once per frame
    void Update()
    {
        // relative poistion of the target (player) based upon the current position
        relativePosition = new Vector2(
            player.GetComponent<Rigidbody2D>().position.x - gameObject.transform.position.x,
            player.GetComponent<Rigidbody2D>().position.y - gameObject.transform.position.y);

        // destroys rocket once it gets out of camera viewport
        if (Camera.main.WorldToViewportPoint(transform.position).y < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        Move();
        StartCoroutine("DestroySelf");
    }

    // logic handling bullet-enemy collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Destroy(this.gameObject);
            GameObject.Destroy(collision.gameObject);
        }
    }

    void Move()
    {
        if (moveSpeed * Time.deltaTime >= Mathf.Abs(relativePosition.x))
        {
            enemyBulletMovement.x = relativePosition.x;
        }
        else
        {
            enemyBulletMovement.x = moveSpeed * Mathf.Sign(relativePosition.x);
        }
        if (moveSpeed * Time.deltaTime >= Mathf.Abs(relativePosition.y))
        {
            enemyBulletMovement.y = relativePosition.y;
        }
        else
        {
            enemyBulletMovement.y = moveSpeed * Mathf.Sign(relativePosition.y);
        }

        rb.velocity = enemyBulletMovement;
    }
    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}