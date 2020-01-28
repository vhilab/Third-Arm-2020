/* =============================================================================
 * Purpose: Tweak the FinalIK VRIK script. Use the OnSwapButtonPress event to
 * give rotation to the foot solvers only when hands and feet are swapped.
 * 
 * Author: Gabriel Mukobi
 * ============================================================================= */

using UnityEngine;
using RootMotion.FinalIK;

public class VRIKAdjuster : MonoBehaviour
{
    VRIK vrik;
    PlayerKeyboardInputDispatcher playerKeyboardInputDispatcher;

    private void Awake()
    {
        vrik = GetComponent<VRIK>();

        playerKeyboardInputDispatcher = GetComponentInParent<PlayerKeyboardInputDispatcher>();
        playerKeyboardInputDispatcher.OnSwapButtonPress += UpdateFootRotationWeightOnSwap;
    }

    private void OnDestroy()
    {
        playerKeyboardInputDispatcher.OnSwapButtonPress -= UpdateFootRotationWeightOnSwap;
    }

    void UpdateFootRotationWeightOnSwap(bool swappedLimbs)
    {
        vrik.solver.leftLeg.rotationWeight = swappedLimbs ? 1.0f : 0.0f;
        vrik.solver.rightLeg.rotationWeight = swappedLimbs ? 1.0f : 0.0f;
    }
}
