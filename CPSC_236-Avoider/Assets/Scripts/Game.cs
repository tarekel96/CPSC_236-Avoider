using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject player;
    public GameObject coinKey;
    public bool resetGame;

    private void Start()
    {
        resetGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (resetGame)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        if (player.GetComponent<BoxCollider2D>().IsTouching(coinKey.GetComponent<BoxCollider2D>()))
        {
            DestroyGameObjects(enemies);
            DestroyGameObjects(enemyBullets);
            Debug.Log("Player has won the game");
            resetGame = true;
            RestartScene();
        }
    }
    void DestroyGameObjects(GameObject[] gameObjects)
    {
        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
    void RestartScene()
    {
        Debug.Log("Won the game ~ application has been quit");
        Application.Quit();
        SceneManager.LoadScene("Menu");
    }
}
