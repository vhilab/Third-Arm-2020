using System.Collections;
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

    public float slerpFactor;

    Vector3 targetRotationEuler;
    float velocity; // needed for Mathf.SmoothDampAngle


    // Update is called once per frame
    void Update()
    {
        // get target rotations
        targetRotationEuler.x = xTarget ? xTarget.rotation.eulerAngles.x : 0;
        targetRotationEuler.y = yTarget ? yTarget.rotation.eulerAngles.y : 0;
        targetRotationEuler.z = zTarget ? zTarget.rotation.eulerAngles.z : 0;

        // smoothly interpolate each component
        Quaternion smoothed = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotationEuler), slerpFactor);

        // update my rotation
        transform.rotation = smoothed;
    }
}
