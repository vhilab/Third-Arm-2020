﻿using System.Collections;
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
    public GameObject[] armModels;
    public MeshRenderer[] armMeshRenderers;
    [SerializeField] private Animator armArnimator;

    public UnityEvent OnInitializeThirdArm;
    public UnityEvent OnThirdArmEnable;
    public UnityEvent OnThirdArmStartDisable;

    private MultiRotationConstraintEulerLerp rotationConstraint;

    private bool isCurrentlyTogglingEnabledState; // acts like a lock
    [HideInInspector] public bool IsArmEnabled { get; private set; }

    private void Awake()
    {
        // zero out z-scale so third arm doesn't appear at first
        gameObject.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0.0f);
    }

    private void Start()
    {
        rotationConstraint = GetComponent<MultiRotationConstraintEulerLerp>();
        OnInitializeThirdArm.Invoke();

        // start with arm disabled
        SetArmModelsActive(false);

        // use only splitHands state for Tribeca film
        if (ThirdArmSettingsReader.Instance.settings.thirdArmBuildType == ThirdArmBuildType.TribecaFilm)
        {
            SetThirdArmState(ThirdArmState.splitHands);
        }
    }

    public void SetThirdArmState(ThirdArmState state)
    {
        // disable multiple state switching for Tribeca film
        if (ThirdArmSettingsReader.Instance.settings.thirdArmBuildType == ThirdArmBuildType.TribecaFilm)
        {
            state = ThirdArmState.splitHands;
        }

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
        {
            return;
        }

        // toggles the arm between the disabled and enabled states using the below coroutines
        if (!IsArmEnabled)
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
        float hmdAngleBelowHorizon = hmd.localEulerAngles.x;
        if (hmdAngleBelowHorizon > 180.0f) hmdAngleBelowHorizon -= 360.0f; // convert to [-180,180]
        if ((ThirdArmSettingsReader.Instance.settings.thirdArmBuildType == ThirdArmBuildType.TribecaFilm) &&
                   hmdAngleBelowHorizon < ThirdArmSettingsReader.Instance.settings.growAngleThresholdHMDEulerX)
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
        SetArmMeshRenderersEnabled(false); // TODO: find a better way to do this
        SetArmModelsActive(true);
        armArnimator.Play("Grow Arm");
        OnThirdArmEnable.Invoke();
        yield return null;
        SetArmMeshRenderersEnabled(true);
        yield return null;

        // wait for animation to finish
        do
        {
            yield return null;
        } while (armArnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
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
        SetArmModelsActive(false);
        yield return null;

        isCurrentlyTogglingEnabledState = false;
    }

    private void SetArmModelsActive(bool active)
    {
        IsArmEnabled = active;
        foreach (GameObject armModel in armModels)
        {
            armModel.SetActive(active);
        }
    }
    private void SetArmMeshRenderersEnabled(bool enabled)
    {
        foreach (MeshRenderer renderer in armMeshRenderers)
        {
            renderer.enabled = enabled;
        }
    }
}
