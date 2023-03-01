using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectile;
    public float shootDelay;
    public ParticleSystem smokeParticles;

    private void Start()
    {
        StartCoroutine(Shoot());
    }
    IEnumerator Shoot()
    {
        

        while (true)
        {
            yield return new WaitForSeconds(shootDelay);
            var _projectile = Instantiate(projectile, launchPoint.position, launchPoint.rotation);
            smokeParticles.Play();
        }
    }


    
}
