/* =============================================================================
 * Purpose: switch between player a model with FinalIK's VRIK script and a player model
 * with custom VRFK script.
 * 
 * Author: Gabriel Mukobi
 * ============================================================================= */

using UnityEngine;
using RootMotion.FinalIK;

public class PlayerKinematicsSwitcher : MonoBehaviour
{
    GameObject vrikGameObject;
    GameObject vrfkGameObject;
    PlayerKeyboardInputDispatcher playerKeyboardInputDispatcher;

    private void Awake()
    {
        vrikGameObject = GetComponentInChildren<VRIK>(includeInactive: true).gameObject;
        vrfkGameObject = GetComponentInChildren<VRFK>(includeInactive: true).gameObject;

        playerKeyboardInputDispatcher = GetComponentInParent<PlayerKeyboardInputDispatcher>();
        playerKeyboardInputDispatcher.OnAnimationTypeButtonPress += ToggleAnimationController;
    }

    private void OnDestroy()
    {
        playerKeyboardInputDispatcher.OnAnimationTypeButtonPress -= ToggleAnimationController;
    }

    private void ToggleAnimationController(PlayerKeyboardInputDispatcher.PlayerAnimType animType)
    {
        if (animType == PlayerKeyboardInputDispatcher.PlayerAnimType.IK)
        {
            vrikGameObject.SetActive(true);
            vrfkGameObject.SetActive(false);
        }
        else  // FK
        {
            vrikGameObject.SetActive(false);
            vrfkGameObject.SetActive(true);
        }
    }

}
