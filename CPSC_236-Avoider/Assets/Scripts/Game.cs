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
    public bool quitGame;

    private void Start()
    {
        resetGame = false;
        quitGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Press the space key to start coroutine
        if (quitGame)
        {
            // Use a coroutine to load the Scene in the background
            StartCoroutine(LoadYourAsyncScene());
            //return;
        }

        if (resetGame)
        {
            return;
        }

        if(player != null)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
            if (player.GetComponent<BoxCollider2D>().IsTouching(coinKey.GetComponent<BoxCollider2D>()))
            {
                DestroyGameObjects(enemies);
                DestroyGameObjects(enemyBullets);
                resetGame = true;
                RestartScene();
            }
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

    public void setQuitTrue()
    {
        quitGame = true;
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Menu");

        // Wait until the asynchronous scene fully loads
        while (true)
        {
            if (asyncLoad.isDone)
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu"));
            else
                yield return null;
        }
    }
}
