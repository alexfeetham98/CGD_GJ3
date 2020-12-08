using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    private GameHandler GH;

    public GameObject pickupEffect;
    // Start is called before the first frame update
    void Start()
    {
        GH = GameObject.Find("Canvas").GetComponent<GameHandler>();

    }

    // Update is called once per frame
    void Update()
    {


        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GH.collectable++;

            Instantiate(pickupEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
