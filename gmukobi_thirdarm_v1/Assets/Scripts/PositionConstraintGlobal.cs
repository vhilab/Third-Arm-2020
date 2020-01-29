using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionConstraintGlobal : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = target.position + offset;
    }
}
