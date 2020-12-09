using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{

    public TextMeshProUGUI collectableText;
    public int collectable;

    void Start()
    {
        collectable = 0;
    }

    // Update is called once per frame
    void Update()
    {
        collectableText.text = collectable.ToString("0");
    }





}
