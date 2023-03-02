using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxProjectile : MonoBehaviour
{
    public float life = 5f;
    private Rigidbody rb;
    public float velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.velocity = transform.forward * velocity * Time.deltaTime * 10;
    }

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent.GetComponent<HealthSystem>().GotHit();
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
