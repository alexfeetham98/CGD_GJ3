using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideParticle : MonoBehaviour
{
    public ParticleSystem PS;

    // Start is called before the first frame update
    void Start()
    {
        PS.enableEmission = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
