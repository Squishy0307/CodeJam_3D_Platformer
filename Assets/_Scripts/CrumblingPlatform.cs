using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour
{
    [Tooltip("How long in seconds this platform takes to crumble.")]
    public float CrumblingTime = 1f;
    private float crumblingCounter = 0f;

    [Tooltip("How long in seconds this platform takes to respawn.")]
    public float RespawnTime = 5f;
    private float respawnCounter = 0f;

    [Tooltip("Particle effect to play when crumbling and respawning.")]
    public GameObject ParticleVFX;
    
    //If the player was on top of the platform.
    private bool playerOnTop = false;
    
    private bool crumbled = false;
    
    private PlayerMovement playerReference;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Whenever the player is found on top of the platform, run this:
        if (playerOnTop)
        {
            //Count the time until limit is reached and it crumbles.
            crumblingCounter += Time.deltaTime;

            if (crumblingCounter >= CrumblingTime)
            {
                playerOnTop = false;
                crumblingCounter = 0f;
                //Make platform crumble
                SetCrumblePlatformState(true);
            }
        }

        //After crumbling, run this instead:
        if (crumbled)
        {
            //Count the time until limit is reached and it respawns.
            respawnCounter += Time.deltaTime;

            if (respawnCounter >= RespawnTime)
            {
                respawnCounter = 0f;
                //Make platform respawn
                SetCrumblePlatformState(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the overlapping object is the player:
        if (other.CompareTag("Player"))
        {
            playerOnTop = true;
        }
    }

    private void SetCrumblePlatformState(bool inValue)
    {
        
        //Disable or enables the visuals and colliders depending of the input value.
        GetComponent<MeshCollider>().enabled = !inValue;
        GetComponent<MeshRenderer>().enabled = !inValue;
        GetComponent<BoxCollider>().enabled = !inValue;
        crumbled = inValue;

        //Spawn particle effect if the VFX variable is valid.
        if (ParticleVFX != null)
        {
            Instantiate(ParticleVFX, transform.position, quaternion.identity);
        }
    }
}
