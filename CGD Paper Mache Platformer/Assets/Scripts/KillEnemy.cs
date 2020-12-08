using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KillEnemy : MonoBehaviour
{
    public CharacterController CharCont;
    private float offset;
    private float enemyHeight = 0;
    public Movement playerScript;
    float KillTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(playerScript.killEnemy)
        {
            KillTimer += Time.deltaTime;
            if(KillTimer >= 0.1f)
            {
                playerScript.killEnemy = false;
                KillTimer = 0;
            }
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Hydrant")
        {
            offset = 0.7f;
            enemyHeight = hit.gameObject.GetComponent<Renderer>().bounds.size.y;

            if ((hit.gameObject.transform.position.y + enemyHeight - offset) <= transform.position.y)
            {
                Debug.Log("Kill Enemy");
                playerScript.killEnemy = true;
               
                playerScript.velocity.y = playerScript.jumpForce;
                Destroy(hit.gameObject);

            }
            else
            {
                Debug.Log("I Die");
            }
        }
        if (hit.gameObject.tag == "Melee")
        {
            offset = 0.9f;
            enemyHeight = hit.gameObject.GetComponent<Renderer>().bounds.size.y;

            if ((hit.gameObject.transform.position.y + enemyHeight - offset) <= transform.position.y)
            {
                Debug.Log("Kill Enemy");
                playerScript.killEnemy = true;

                playerScript.velocity.y = playerScript.jumpForce;
                Destroy(hit.gameObject);

            }
            else
            {
                Debug.Log("I Die");
            }
        }
        if (hit.gameObject.tag == "Ranged")
        {
            offset = 2.6f;
            enemyHeight = hit.gameObject.GetComponent<Renderer>().bounds.size.y;

            if ((hit.gameObject.transform.position.y + enemyHeight - offset) <= transform.position.y)
            {
                Debug.Log("Kill Enemy");
                playerScript.killEnemy = true;

                playerScript.velocity.y = playerScript.jumpForce;
                Destroy(hit.gameObject);

            }
            else
            {
                Debug.Log("I Die");
            }
        }
    }
}
