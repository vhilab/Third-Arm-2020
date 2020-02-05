using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandleThirdArmStateInput : MonoBehaviour
{
    [Tooltip("The third arm to control.")]
    public ThirdArmStateChanger thirdArm;

    public SteamVR_Action_Boolean cycleThirdArmStateAction;

    private void Update()
    {
        // handle 1-4 keyboard input
        if (Input.GetKeyDown(KeyCode.Alpha1))
            thirdArm.SetThirdArmState(ThirdArmState.disabled);
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //    thirdArm.SetThirdArmState(ThirdArmState.followHmd); // DEPRECATED for Tribeca lab tour project
        if (Input.GetKeyDown(KeyCode.Alpha3))
            thirdArm.SetThirdArmState(ThirdArmState.splitHands);
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //    thirdArm.SetThirdArmState(ThirdArmState.leftHandOnly); // DEPRECATED for Tribeca lab tour project

        // handle cycle input via keyboard or controller
        if (cycleThirdArmStateAction.GetStateDown(SteamVR_Input_Sources.Any)
            || Input.GetKeyDown(KeyCode.Space))
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
