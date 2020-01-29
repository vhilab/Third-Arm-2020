﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiRotationConstraintEulerLerp : MonoBehaviour
{
    [Tooltip("Target for the euler-x rotation (leave None to not constrain x)")]
    public Transform xTarget;
    [Tooltip("Target for the euler-y rotation (leave None to not constrain y)")]
    public Transform yTarget;
    [Tooltip("Target for the euler-z rotation (leave None to not constrain z)")]
    public Transform zTarget;

    public float xLerpFactor;
    public float yLerpFactor;
    public float zLerpFactor;


    Vector3 targetRotationEuler;

    // Update is called once per frame
    void FixedUpdate()
    {
        // get target rotations
        targetRotationEuler.x = xTarget ? xTarget.rotation.eulerAngles.x : transform.rotation.eulerAngles.x;
        targetRotationEuler.y = yTarget ? yTarget.rotation.eulerAngles.y : transform.rotation.eulerAngles.y;
        targetRotationEuler.z = zTarget ? zTarget.rotation.eulerAngles.z : transform.rotation.eulerAngles.z;

        // lerp each way
        targetRotationEuler.x = Mathf.Lerp(transform.rotation.eulerAngles.x, targetRotationEuler.x, xLerpFactor);
        targetRotationEuler.y = Mathf.Lerp(transform.rotation.eulerAngles.y, targetRotationEuler.y, yLerpFactor);
        targetRotationEuler.z = Mathf.Lerp(transform.rotation.eulerAngles.z, targetRotationEuler.z, zLerpFactor);

        // update my rotation
        transform.rotation = Quaternion.Euler(targetRotationEuler);
    }
}
