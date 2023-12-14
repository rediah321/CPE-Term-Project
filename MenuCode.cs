using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuCode : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("SampleScene");

    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
