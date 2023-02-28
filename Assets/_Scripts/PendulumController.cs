using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PendulumController : MonoBehaviour
{
    public float TargetAngle = 45f;

    [Tooltip("How long a single swing should last for.")]
    public float SwingDuration = 3f;

    private float currentAngle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //Makes sure the starting and target angle are equidistant to 0, so the swing motion is perfect in both ways.
        TargetAngle = Mathf.Abs(TargetAngle);
        currentAngle = -TargetAngle;
        
        //Set the starting rotation of the swinging pendulum;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -TargetAngle);
        
        //Updates the current angle value in the Z axis, going back and forth from -TargetAngle to +TargetAngle.
        DOTween.To(() => currentAngle, x => currentAngle = x, TargetAngle, 3f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    // Update is called once per frame
    void Update()
    {
        //Sets the current angle value in the Z axis to the actual rotation of the object.
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, currentAngle);
    }
}
