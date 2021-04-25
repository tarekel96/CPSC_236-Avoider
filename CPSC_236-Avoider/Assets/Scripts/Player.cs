using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    
    public Vector2 clickedPosition; // cursor clicked position
    public Vector2 relativePosition;  // relativePosition - character relative position
    private Vector2 movement;  // movement - var that stores movement position after a click


    void Update()
    {
        // set the mouse position
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        // relative poistion of the target based upon the current position
        relativePosition = new Vector2(
            clickedPosition.x - gameObject.transform.position.x,
            clickedPosition.y - gameObject.transform.position.y);
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (moveSpeed * Time.deltaTime >= Mathf.Abs(relativePosition.x))
        {
            movement.x = relativePosition.x;
        }
        else
        {
            movement.x = moveSpeed * Mathf.Sign(relativePosition.x);
        }
        if (moveSpeed * Time.deltaTime >= Mathf.Abs(relativePosition.y))
        {
            movement.y = relativePosition.y;
        }
        else
        {
            movement.y = moveSpeed * Mathf.Sign(relativePosition.y);
        }

        rb.velocity = movement;
    }

}
