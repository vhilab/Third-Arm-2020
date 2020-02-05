using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThirdArmState
{
    disabled,
    followHmd,
    splitHands, // left hand heading, right hand pitch
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

    public void ToggleThirdArmOnOrOff()
    {
        // toggles the arm between the disabled and the splitHands states using the below coroutines
        if (CurrentState == ThirdArmState.disabled)
        {
            GrowThirdArm();
        } 
        else  // shrink from any non-disabled state
        {
            ShrinkThirdArm();
        }
    }

    public void GrowThirdArm()
    {
        StartCoroutine(GrowThirdArmCoroutine());
    }

    private IEnumerator GrowThirdArmCoroutine()
    {
        // Tets the third arm to the splitHands state then grows it.
        // TODO: grow that thing using an animation
        SetThirdArmState(ThirdArmState.splitHands);
        yield return null;
    }

    public void ShrinkThirdArm()
    {
        ShrinkThirdArmCoroutine();
    }

    private IEnumerator ShrinkThirdArmCoroutine()
    {
        // Shrinks the third arm and sets it to the disabled state.
        // TODO: shrink using an animation
        SetThirdArmState(ThirdArmState.disabled);
        yield return null;
    }
}
