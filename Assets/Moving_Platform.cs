using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] float moveSpd = 2f;
    [SerializeField] Rigidbody[] movePoints;
    private int currentPoint = 1;
    Vector3 dir;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        dir = movePoints[currentPoint].position - transform.position;
        Debug.Log(dir);
        rb.velocity = transform.up * moveSpd * 10 * Time.deltaTime;
        Debug.Log(rb.velocity);

        if(Vector3.Distance(transform.position,movePoints[currentPoint].position) <= 0.1f)
        {
            transform.position = movePoints[currentPoint].position;
            currentPoint++;
            moveSpd *= -1f;

            if(currentPoint >= movePoints.Length)
            {
                currentPoint = 0;
            }
        }
    }
}
