using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public enum ThirdArmState
{
    followHmd,
    splitHands, // left hand heading, right hand pitch
    leftUpRightTwist,
};

public class ThirdArmStateChanger : MonoBehaviour
{
    public Transform hmd;
    public Transform controllerLeft;
    public Transform controllerRight;
    public GameObject armModel;
    [SerializeField] private Animator armArnimator;

    public UnityEvent OnThirdArmEnable;
    public UnityEvent OnThirdArmDisable;

    private MultiRotationConstraintEulerLerp rotationConstraint;
    private MeshRenderer armMeshRenderer;

    private void Start()
    {
        rotationConstraint = GetComponent<MultiRotationConstraintEulerLerp>();
        SetArmModelActive(false);

        armMeshRenderer = armModel.GetComponent<MeshRenderer>();
    }

    public void SetThirdArmState(ThirdArmState state)
    {
        switch (state)
        {
            case ThirdArmState.followHmd:
                rotationConstraint.xTarget = hmd;
                rotationConstraint.yTarget = hmd;
                rotationConstraint.yTargetButUseZRot = null;
                break;
            case ThirdArmState.splitHands:
                rotationConstraint.xTarget = controllerRight;
                rotationConstraint.yTarget = controllerLeft;
                rotationConstraint.yTargetButUseZRot = null;
                break;
            case ThirdArmState.leftUpRightTwist:
                rotationConstraint.xTarget = controllerLeft;
                rotationConstraint.yTarget = hmd;  // base y rotation
                rotationConstraint.yTargetButUseZRot = controllerRight;  // twist rotation added to y
                break;
        }
    }

    private void SetArmModelActive(bool active)
    {
        armModel.SetActive(active);
        if (active)
        {
            OnThirdArmEnable.Invoke();
        } 
        else
        {
            OnThirdArmDisable.Invoke();
        }
    }

    public void ToggleThirdArmOnOrOff()
    {
        // toggles the arm between the disabled and enabled states using the below coroutines
        if (!armModel.activeSelf)
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
        // Sets the third arm to the active state then grows it.

        // don't render initially so it doesn't pop in for a frame the first time it grows
        armMeshRenderer.enabled = false;  // TODO: find a better way to do this
        SetArmModelActive(true);
        armArnimator.Play("Grow Arm");
        yield return null;
        armMeshRenderer.enabled = true;
        yield return null;
    }

    public void ShrinkThirdArm()
    {
        StartCoroutine(ShrinkThirdArmCoroutine());
    }

    private IEnumerator ShrinkThirdArmCoroutine()
    {
        // Shrinks the third arm and sets it to the inactive state.

        //Debug.Log("Playing shrinking animation");
        armArnimator.Play("Shrink Arm");
        // wait for animation to finish
        do
        {
            yield return null;
        } while (armArnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        //Debug.Log("Done with shrinking animation");
        SetArmModelActive(false);
        yield return null;
    }
}
