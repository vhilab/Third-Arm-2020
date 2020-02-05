using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public enum ThirdArmState
{
    disabled,
    // followHmd, // DEPRECATED for Tribeca lab tour project
    splitHands, // left hand heading, right hand pitch
    // leftHandOnly // DEPRECATED for Tribeca lab tour project
};

public class ThirdArmStateChanger : MonoBehaviour
{
    // public Transform hmd; // DEPRECATED for Tribeca lab tour project
    public Transform controllerLeft;
    public Transform controllerRight;
    public GameObject armModel;
    [SerializeField] private Animator armArnimator;

    public UnityEvent OnThirdArmEnable;
    public UnityEvent OnThirdArmDisable;

    public ThirdArmState CurrentState { get; private set; }

    private MultiRotationConstraintEulerLerp rotationConstraint;

    private MeshRenderer armMeshRenderer;

    private void Start()
    {
        rotationConstraint = GetComponent<MultiRotationConstraintEulerLerp>();
        SetThirdArmState(ThirdArmState.disabled);

        armMeshRenderer = armModel.GetComponent<MeshRenderer>();
    }

    public void SetThirdArmState(ThirdArmState state)
    {
        CurrentState = state;
        switch (state)
        {
            case ThirdArmState.disabled:
                OnThirdArmDisable.Invoke();
                armModel.SetActive(false);
                break;
            //case ThirdArmState.followHmd:
            //    thirdArm.gameObject.SetActive(true);
            //    thirdArm.xTarget = hmd;
            //    thirdArm.yTarget = hmd;
            //    break; // DEPRECATED for Tribeca lab tour project
            case ThirdArmState.splitHands:
                armModel.SetActive(true);
                rotationConstraint.xTarget = controllerRight;
                rotationConstraint.yTarget = controllerLeft;
                OnThirdArmEnable.Invoke();
                break;
            //case ThirdArmState.leftHandOnly:
            //    thirdArm.gameObject.SetActive(true);
            //    thirdArm.xTarget = controllerLeft;
            //    thirdArm.yTarget = controllerLeft;
            //    break; // DEPRECATED for Tribeca lab tour project
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
        // Sets the third arm to the splitHands state then grows it.

        // don't render initially so it doesn't pop in for a frame the first time it grows
        armMeshRenderer.enabled = false;  // TODO: find a better way to do this

        SetThirdArmState(ThirdArmState.splitHands);
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
        // Shrinks the third arm and sets it to the disabled state.

        //Debug.Log("Playing shrinking animation");
        armArnimator.Play("Shrink Arm");
        // wait for animation to finish
        do
        {
            yield return null;
        } while (armArnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        //Debug.Log("Done with shrinking animation");
        SetThirdArmState(ThirdArmState.disabled);
        yield return null;
    }
}
