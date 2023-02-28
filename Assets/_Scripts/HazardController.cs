using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HazardController : MonoBehaviour
{
    public bool SelfDestruct = false;

    public GameObject HitSFX;

    private void OnTriggerEnter(Collider other)
    {
        //If the particle effect is set, spawn it when it hits something.
        if (HitSFX != null)
        {
            Instantiate(HitSFX, other.transform.position, quaternion.identity);
        }
        
        //If overlapped with the player object:
        if (other.CompareTag("Player"))
        {
            HealthSystem temp = other.transform.parent.GetComponent<HealthSystem>();
            temp.GotHit();
        }
        
        //Destroy the hazard object if the variable says so.
        if (SelfDestruct)
        {
            Destroy(gameObject);
        }
        
    }
}
