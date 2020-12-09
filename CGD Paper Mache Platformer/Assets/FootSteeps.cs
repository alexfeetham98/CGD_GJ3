using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteeps : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string inputsounds;
    bool playismoving;
    public float walkingspeed;

    void Update()
    {
        if (Input.GetAxis("Vertical") >= 0.01f || Input.GetAxis("Horizontal") >= 0.01f || Input.GetAxis("Vertical") <= -0.01f || Input.GetAxis("Horizontal") <= -0.01f)
        {
            playismoving = true;
        }
        else if (Input.GetAxis("Vertical") == 0 || Input.GetAxis("Horizontal") == 0)
        {
            playismoving = false;
        }
    }

    void CallFootsteps()
    {
        if (playismoving == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot(inputsounds);
        }
    }

    void Start()
    {
        InvokeRepeating("CallFootsteps", 0, walkingspeed);
    }

    void OnDisabled()
    {
        playismoving = false;
    }
}
