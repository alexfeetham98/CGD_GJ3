using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public bool isFireHydrant = false;
    public float OnOffTime = 2f;
    public GameObject player;
    public float aggroRange = 20;
    public float shootingRange = 10;
    public float range = 0;
    public float rotationSpeed = 5;
    public Transform[] patrol;
    public int patrolPoint = 0;
    public Collider splashCollider;
    public ParticleSystem splashParticles;
    private NavMeshAgent nma;
    private float shootTimer = 0f;
    private float PshootTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFireHydrant)
        {
            PeriodicFire();
        }
        else
        {
            Aggro();
        }
       
    }

    void Aggro()
    {
        range = Vector3.Distance(player.transform.position, transform.position);
        if (range < shootingRange)
        {
            nma.SetDestination(transform.position);
            FaceTarget();
            Shoot();
        }
        else if (range < aggroRange)
        {
            nma.SetDestination(player.transform.position);
            
        }
        else
        {
            if (nma.remainingDistance <= 0.1f && !nma.pathPending)
            {
                Patrolling();
            }
        }
        if (range >= shootingRange)
        {
            splashCollider.enabled = false;
            splashParticles.Stop();
            shootTimer = 0f;
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    void Shoot()
    {
       
       splashParticles.Play();
       shootTimer += Time.deltaTime;
        if (shootTimer >= 0.3f)
        {
            splashCollider.enabled = true;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Water");
        }
    }

    void Patrolling()
    {
        if(patrol.Length == 0)
        {
            nma.SetDestination(transform.position);
            return;
        }

        nma.destination = patrol[patrolPoint].position;
        patrolPoint = (patrolPoint + 1) % patrol.Length;
    }

    void PeriodicFire()
    {
        PshootTimer += Time.deltaTime;

        if (PshootTimer <= OnOffTime)
        {
            splashParticles.Play();
            if (PshootTimer >= 0.3f)
            {
                splashCollider.enabled = true;
            }
        }
        else if(PshootTimer >= OnOffTime)
        {
            splashParticles.Stop();
            splashCollider.enabled = false;

            if(PshootTimer >= 4f)
            {
                PshootTimer = 0f; 
            }
        }
    }
}