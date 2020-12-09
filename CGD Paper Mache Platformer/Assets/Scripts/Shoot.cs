using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public Health health;
    float timer = 0.0f;
    bool canHit = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!canHit)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                canHit = true;
                timer = 1.0f;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (canHit)
        {
            if (other.tag == "Player")
            {
                timer = 1.0f;
                canHit = false;
                Debug.Log("Damage");
                health.lives--;
                return;
            }
        }
        
    }
}
