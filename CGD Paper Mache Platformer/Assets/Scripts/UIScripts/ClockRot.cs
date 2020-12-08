using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockRot : MonoBehaviour
{
    float rot;
    float rot_speed = 200;

    // Update is called once per frame
    void Update()
    {
        rot += Time.deltaTime * rot_speed;

        transform.rotation = Quaternion.Euler(0, 0, rot);
    }
}
