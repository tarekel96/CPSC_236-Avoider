using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 7.5f;
    
    public Vector2 clickedPosition; // cursor clicked position
    public Vector2 relativePosition;  // relativePosition - character relative position
    private Vector2 movement;  // movement - var that stores movement position after a click

    private float firstClickTime, timeBetweenClicks;
    private bool coroutineAllowed;
    private int clickCounter;

    private void Start()
    {
        firstClickTime = 0f;
        timeBetweenClicks = 0.2f;
        clickCounter = 0;
        coroutineAllowed = true;
    }

    void Update()
    {

        // set the mouse position
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            clickCounter++;
            clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if(clickCounter == 1 && coroutineAllowed)
        {
            moveSpeed = 7.5f;
            firstClickTime = Time.time;
            StartCoroutine(DoubleClickDetection());
        }


        // relative poistion of the target based upon the current position
        relativePosition = new Vector2(
            clickedPosition.x - gameObject.transform.position.x,
            clickedPosition.y - gameObject.transform.position.y);
    }

    private IEnumerator DoubleClickDetection()
    {
        coroutineAllowed = false;
        while(Time.time < firstClickTime + timeBetweenClicks)
        {
            if(clickCounter == 2)
            {
                Debug.Log("Double Click");
                moveSpeed = 15f;
                yield return new WaitForSeconds(1.5f); // give player 1.5x speed for 1.5 seconds
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
            
        }
        clickCounter = 0;
        firstClickTime = 0f;
        coroutineAllowed = true;
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
