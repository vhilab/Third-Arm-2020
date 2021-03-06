﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandleThirdArmInput : MonoBehaviour
{
    [Tooltip("The third arm to control.")]
    public ThirdArmStateChanger thirdArm;

    [Tooltip("Disable to only accept keyboard input (e.g. for lab demo).")]
    [HideInInspector] private bool acceptVRControllerInput;
    public SteamVR_Action_Boolean cycleThirdArmStateSteamVRAction;

    [SerializeField] private SwitchActiveGameObjects thirdArmModelSwitcher = default;
    [SerializeField] private ToggleActive mirrorToggler = default;

    [SerializeField] private ScaleBasedOnHeight avatarHeightScaler = default;

    private void Start()
    {
        acceptVRControllerInput = ThirdArmSettingsReader.Instance.settings.thirdArmBuildType != ThirdArmBuildType.LabDemo;
    }

    private void Update()
    {
        // handle 1-4 keyboard input // not for Tribeca lab tour project
        #region
        if (Input.GetKeyDown(KeyCode.Alpha1))
            thirdArm.SetThirdArmState(ThirdArmState.followHmd);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            thirdArm.SetThirdArmState(ThirdArmState.splitHands);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            thirdArm.SetThirdArmState(ThirdArmState.leftUpRightTwist);
        #endregion

        // toggle the model of the third arm (e.g. human and robotic)
        if (Input.GetKeyDown(KeyCode.A))
            thirdArmModelSwitcher.IterateNextActiveObject();

        // toggle mirror
        if (Input.GetKeyDown(KeyCode.M))
            mirrorToggler.ToggleGameObjectActive();

        // recalibrate avatar scale
        if (Input.GetKeyDown(KeyCode.H))
            avatarHeightScaler.Scale();

        // handle cycle input via keyboard or controller
        if ((acceptVRControllerInput && cycleThirdArmStateSteamVRAction.GetStateDown(SteamVR_Input_Sources.Any)) // controller input
            || Input.GetKeyDown(KeyCode.Space)) // keyboard input
            thirdArm.ToggleThirdArmOnOrOff();
    }

    //void CycleThirdArmState()
    //{
    //    // Cycles through all 4 states.
    //    int totalNumStates = System.Enum.GetNames(typeof(ThirdArmState)).Length;
    //    //Debug.Log($"totalNumStates: {totalNumStates}");
    //    int nextState = ((int)thirdArm.CurrentState + 1) % totalNumStates;

    //    thirdArm.SetThirdArmState((ThirdArmState)nextState);
    //    //Debug.Log($"(ThirdArmState)nextState: {(ThirdArmState)nextState}");
    //} // DEPRECATED for Tribeca lab tour project
}
