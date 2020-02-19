using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiRotationConstraintEulerLerp : MonoBehaviour
{
    [Tooltip("Target for the euler-x rotation (leave None to not constrain x)")]
    public Transform xTarget;
    [Tooltip("Target for the euler-y rotation (leave None to not constrain y)")]
    public Transform yTarget;
    //[Tooltip("Target for the euler-z rotation (leave None to not constrain z)")]
    //public Transform zTarget; // deprecated because not used
    [Tooltip("Target for the euler-y rotation but uses the z-component of the " +
        "target. Adds on to yTarget if set (leave None to not constrain y). " +
        "Hacked in here for ThirdArmState.leftUpRightTwist")]
    public Transform yTargetButUseZRot;

    public float leftUpRightTwistZScalingFactor;

    public float slerpFactor;

    Vector3 targetRotationEuler;
    float velocity; // needed for Mathf.SmoothDampAngle


    // Update is called once per frame
    void Update()
    {
        // get target rotations
        targetRotationEuler.x = xTarget ? xTarget.rotation.eulerAngles.x : 0;
        targetRotationEuler.y = yTarget ? yTarget.rotation.eulerAngles.y : 0;
        if (yTargetButUseZRot)
        {
            float additionalYRot = yTargetButUseZRot.rotation.eulerAngles.z;
            // put rotation in range [-180, 180]
            additionalYRot = additionalYRot > 180.0f ? additionalYRot - 360.0f : additionalYRot;
            additionalYRot = additionalYRot < -180.0f ? additionalYRot + 360.0f : additionalYRot;
            targetRotationEuler.y += additionalYRot * leftUpRightTwistZScalingFactor;
        }

        // smoothly interpolate each component
        Quaternion smoothed = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotationEuler), slerpFactor);

        // update my rotation
        transform.rotation = smoothed;
    }
}
