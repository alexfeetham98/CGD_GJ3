using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnarea : MonoBehaviour
{
    public GameObject Player;
    public GameObject Respawn;
    private CharacterController controller;
    public Health health;

    void Start()
    {
    }

    void Update()
    {
        
    } 

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.gameObject.tag == "Player")
        {
            controller = other.gameObject.GetComponent<CharacterController>();
            controller.enabled = false;
            Player.transform.position = Respawn.transform.position;
            controller.enabled = true;
            health.lives--;
        }
            
    }


}

