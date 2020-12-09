using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Health : MonoBehaviour
{
    public int lives = 3;

    public TextMeshProUGUI txt;

    void Update()
    {
        txt.text = lives.ToString("");

        if (lives <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}