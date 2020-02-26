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

    public UnityEvent OnInitializeThirdArm;
    public UnityEvent OnThirdArmEnable;
    public UnityEvent OnThirdArmStartDisable;

    private MultiRotationConstraintEulerLerp rotationConstraint;
    private MeshRenderer armMeshRenderer;

    private bool isCurrentlyTogglingEnabledState = false; // acts like a lock

    private void Awake()
    {
        // zero out z-scale so third arm doesn't appear at first
        gameObject.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0.0f);
    }

    private void Start()
    {
        rotationConstraint = GetComponent<MultiRotationConstraintEulerLerp>();
        armModel.SetActive(false);
        armMeshRenderer = armModel.GetComponent<MeshRenderer>();
        OnInitializeThirdArm.Invoke();
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

    public void ToggleThirdArmOnOrOff()
    {
        // Prevent third arm state toggling while state is being toggled
        if (isCurrentlyTogglingEnabledState)
            return;

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
        if (ThirdArmSettingsReader.Instance.settings.thirdArmBuildType == ThirdArmBuildType.TribecaFilm &&
                   hmd.localEulerAngles.x < ThirdArmSettingsReader.Instance.settings.growAngleThresholdHMDEulerX)
        {
            // don't grow if not looking down 
            return;
        }
        StartCoroutine(GrowThirdArmCoroutine());
    }

    public void ShrinkThirdArm()
    {
        StartCoroutine(ShrinkThirdArmCoroutine());
    }

    private IEnumerator GrowThirdArmCoroutine()
    {
        // Sets the third arm to the active state then grows it.
        isCurrentlyTogglingEnabledState = true;

        // don't render initially so it doesn't pop in for a frame the first time it grows
        armMeshRenderer.enabled = false;  // TODO: find a better way to do this
        armModel.SetActive(true);
        OnThirdArmEnable.Invoke();
        armArnimator.Play("Grow Arm");
        yield return null;
        armMeshRenderer.enabled = true;
        yield return null;

        isCurrentlyTogglingEnabledState = false;
    }

    private IEnumerator ShrinkThirdArmCoroutine()
    {
        // Shrinks the third arm and sets it to the inactive state.
        isCurrentlyTogglingEnabledState = true;

        //Debug.Log("Playing shrinking animation");
        armArnimator.Play("Shrink Arm");
        OnThirdArmStartDisable.Invoke();
        // wait for animation to finish
        do
        {
            yield return null;
        } while (armArnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        //Debug.Log("Done with shrinking animation");
        armModel.SetActive(false);
        yield return null;

        isCurrentlyTogglingEnabledState = false;
    }
}
