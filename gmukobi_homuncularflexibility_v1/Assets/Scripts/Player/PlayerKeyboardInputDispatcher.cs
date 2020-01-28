/* =============================================================================
 * Purpose: Dispatch events for limb swapping, player animation solver type (IK/FK)
 * swapping, or both at the same time based on keyboard input.
 * 
 * Author: Gabriel Mukobi
 * ============================================================================= */

using System;
using UnityEngine;
using Valve.VR;

public class PlayerKeyboardInputDispatcher : MonoBehaviour
{
    [SerializeField]
    private SteamVR_Action_Boolean swapAction;

    public event Action<bool> OnSwapButtonPress = delegate { };
    public event Action<PlayerAnimType> OnAnimationTypeButtonPress = delegate { };

    bool swappedLimbs = false;
    PlayerAnimType animType = PlayerAnimType.IK;
    public enum PlayerAnimType
    {
        IK,
        FK
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) ||
            swapAction.GetStateDown(SteamVR_Input_Sources.Any))
        {
            Debug.Log("Swapping limbs and animation type!");
            // swap limbs
            swappedLimbs = !swappedLimbs;
            OnSwapButtonPress(swappedLimbs);
            // FK when swapped, IK when unswapped
            animType = swappedLimbs ? PlayerAnimType.FK : PlayerAnimType.IK;
            OnAnimationTypeButtonPress(animType);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Swapping limbs!");
            swappedLimbs = !swappedLimbs;
            OnSwapButtonPress(swappedLimbs);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Swapping animation type!");
            animType = animType == PlayerAnimType.IK ? PlayerAnimType.FK : PlayerAnimType.IK;
            OnAnimationTypeButtonPress(animType);
        }
    }
}
