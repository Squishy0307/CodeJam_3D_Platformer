using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public GameObject scoreText;
    public static int theScore;

    public ParticleSystem collisionParticleSystem;
    public MeshRenderer mr;
    public bool once = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && once)
        {
            var em = collisionParticleSystem.emission;
            var dur = collisionParticleSystem.duration;

            em.enabled = true;
            collisionParticleSystem.Play();

            once = false;
            Destroy(mr);
            Invoke(nameof(DestroyObj), dur);
        }

        theScore++;
        scoreText.GetComponent<Text>().text = "" + theScore;
        //transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    void DestroyObj()
    {
        Destroy(gameObject);
    }
}
