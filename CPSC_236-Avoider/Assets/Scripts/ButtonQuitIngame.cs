using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonQuitIngame : MonoBehaviour
{

    public void HandleClick()
    {
        Application.Quit();
        Debug.Log("Quit the Game");
        SceneManager.LoadScene(0);
    }

}
