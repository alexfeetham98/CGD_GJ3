using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    private GameHandler GH;

    // Start is called before the first frame update
    void Start()
    {
        GH = GameObject.Find("Canvas").GetComponent<GameHandler>();

    }

    private void OnTriggerEnter(Collider other)
    {
        GH.collectable++;
        Destroy(gameObject);
    }
}
