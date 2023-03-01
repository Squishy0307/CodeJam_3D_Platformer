using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 Axis;

    void Update()
    {
        transform.Rotate(Axis * 10 * Time.deltaTime);
    }

}
