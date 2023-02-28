using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public int yAxis;

    void Update()
    {
        transform.Rotate(new Vector3(0, yAxis*10, 0) * Time.deltaTime);
    }
}
