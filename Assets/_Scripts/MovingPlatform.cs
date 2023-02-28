using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    //private Rigidbody rb;

    [SerializeField] bool verticalMover; //v = 11.58  h = 10.15
    [SerializeField] float velMultiplier;
    [SerializeField] float waitBetweenPoints = 0.5f;
    private int currentPoint;
    private bool playerIsOnTop;
    private Rigidbody playerRb;
    private PlayerMovement playerMover;

    [SerializeField] float MoveSpeed = 2f;
    [SerializeField] Transform[] MovePoints;
    [SerializeField] GameObject dots;

    private Vector3 oldPosition;
    private Vector3 velocity;
    private bool velStoppedOnce = false;

    void Start()
    {
        moveToNextPoint();
        oldPosition = transform.position;
        //rb = GetComponent<Rigidbody>();

        if (verticalMover)
        {
            velMultiplier = 11.58f;
        }
        else
        {
            velMultiplier = 16.2f;
        }

    }

    private void FixedUpdate()
    {
        var newPosition = transform.position; ;
        var media = newPosition - oldPosition;

        velocity = (media / Time.fixedDeltaTime).normalized;
        oldPosition = newPosition;

        if (playerIsOnTop)
        {
            if (verticalMover)
            {
                playerRb.AddForce(velocity * velMultiplier, ForceMode.Force);
            }

            else
            {
                if (Mathf.Abs(velocity.x) == 0 && !velStoppedOnce)
                {
                    velStoppedOnce = true;
                    playerRb.velocity = new Vector3(0, playerRb.velocity.y, 0);
                }
                else
                {
                    velStoppedOnce = false;
                }

                playerRb.AddForce(velocity * velMultiplier, ForceMode.Force);

            }           
        }

    }

    void moveToNextPoint()
    {
        Sequence mover = DOTween.Sequence();

        mover.Append(transform.DOMove(MovePoints[currentPoint].position, calculateTime(MovePoints[currentPoint])).SetEase(Ease.Linear));
        mover.AppendInterval(waitBetweenPoints).OnComplete(moveToNextPoint);

        currentPoint++;
        if(currentPoint > MovePoints.Length -1)
        {
            currentPoint = 0;
        }
    }

    float calculateTime(Transform point)
    {
        float distance = Vector3.Distance(transform.position, point.position);
        float time = distance / MoveSpeed;
        return time;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            playerRb = other.transform.parent.GetComponent<Rigidbody>();
            playerMover = other.transform.parent.GetComponent<PlayerMovement>();

            playerIsOnTop = true;
            playerMover.movingPlatformHandler(true, verticalMover);
            playerRb.useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsOnTop = false;
            playerMover.movingPlatformHandler(false, verticalMover);
            playerRb.useGravity = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(MovePoints.Length >= 2)
        {
            for (int i = 0; i < MovePoints.Length - 1; i++)
            {
                Gizmos.DrawLine(MovePoints[i].position, MovePoints[i + 1].position);
                Gizmos.DrawSphere(MovePoints[i].position, 0.2f);
                Gizmos.DrawSphere(MovePoints[i + 1].position, 0.2f);
            }            
        }
    }
}
