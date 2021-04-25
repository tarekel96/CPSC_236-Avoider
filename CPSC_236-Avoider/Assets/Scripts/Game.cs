using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject player;
    public GameObject coinKey;
    private bool resetGame = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        if (player.GetComponent<BoxCollider2D>().IsTouching(coinKey.GetComponent<BoxCollider2D>()))
        {
            DestroyGameObjects(enemies);
            DestroyGameObjects(enemyBullets);
            Debug.Log("Player has won the game");
        }
    }
    void DestroyGameObjects(GameObject[] gameObjects)
    {
        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
}
