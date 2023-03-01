using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [Range(100, 10000)]
    public float bounceHeight;

  

    private void OnTriggerEnter(Collider other)
    {

        Rigidbody rb = other.transform.parent.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * bounceHeight);
        Debug.Log("Bouncing");
    }

}
