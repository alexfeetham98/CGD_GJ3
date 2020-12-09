using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        // load level
        SceneManager.LoadScene("CompletedLevel");
    }

    public void Quit()
    {
        // quit
        Application.Quit();
    }
}
