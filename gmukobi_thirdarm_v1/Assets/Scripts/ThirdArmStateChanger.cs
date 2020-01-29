using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThirdArmState
{
    disabled,
    followHmd,
    splitHands,
    leftHandOnly
};

public class ThirdArmStateChanger : MonoBehaviour
{
    public Transform hmd;
    public Transform controllerLeft;
    public Transform controllerRight;

    public ThirdArmState CurrentState { get; private set; }

    private MultiRotationConstraintEulerLerp thirdArm;

    private void Start()
    {
        thirdArm = GetComponent<MultiRotationConstraintEulerLerp>();
        SetThirdArmState(ThirdArmState.disabled);
    }

    public void SetThirdArmState(ThirdArmState state)
    {
        CurrentState = state;
        switch (state)
        {
            case ThirdArmState.disabled:
                thirdArm.gameObject.SetActive(false);
                break;
            case ThirdArmState.followHmd:
                thirdArm.gameObject.SetActive(true);
                thirdArm.xTarget = hmd;
                thirdArm.yTarget = hmd;
                break;
            case ThirdArmState.splitHands:
                thirdArm.gameObject.SetActive(true);
                thirdArm.xTarget = controllerRight;
                thirdArm.yTarget = controllerLeft;
                break;
            case ThirdArmState.leftHandOnly:
                thirdArm.gameObject.SetActive(true);
                thirdArm.xTarget = controllerLeft;
                thirdArm.yTarget = controllerLeft;
                break;
        }
    }
}
