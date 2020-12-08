using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMovement : MonoBehaviour
{

    private float tempVal;
    private Vector3 tempPos;
    private float amplitude = 0.25f;
    private float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {

        tempPos = transform.position;
        tempVal = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        /*tempPos.y = tempVal + amplitude * Mathf.Sin(speed * Time.time);
        transform.position = tempPos;*/

        //this makes the object rotate
        this.transform.Rotate(0, 50 * Time.deltaTime, 0);
    }
}
