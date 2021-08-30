using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuChanger : MonoBehaviour
{
    public void sceneChanger(int idScene)
    {
        SceneManager.LoadScene(idScene);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
