/* =============================================================================
 * Purpose: swap the target of the FollowObjectWorldOffset component with an
 * alternative swappedTarget, allowing for the swapping of hands and feet by
 * changing which transform the target GameObjects follow.
 * 
 * Author: Gabriel Mukobi
 * ============================================================================= */

using UnityEngine;

public class SwapHandsAndFeet : MonoBehaviour
{
    public Transform swappedTarget;
    public Vector3 swapWorldTranslation;

    Transform unswappedTarget;
    FollowObjectWorldOffset followObjectWorldOffset;
    PlayerKeyboardInputDispatcher playerKeyboardInputDispatcher;

    // Start is called before the first frame update
    void Start()
    {
        followObjectWorldOffset = GetComponent<FollowObjectWorldOffset>();
        unswappedTarget = followObjectWorldOffset.target;

        playerKeyboardInputDispatcher = GetComponentInParent<PlayerKeyboardInputDispatcher>();
        playerKeyboardInputDispatcher.OnSwapButtonPress += SwapLimbs;
    }

    private void OnDestroy()
    {
        playerKeyboardInputDispatcher.OnSwapButtonPress -= SwapLimbs;
    }

    void SwapLimbs(bool swapped)
    { 
        if (swapped)
        {
            // swap stuff
            SwapPositions(swapped);
            SwapIKTargets(swapped);
        }
        else
        {
            // reset to original value by unswapping in reverse order
            SwapIKTargets(swapped);
            SwapPositions(swapped);
        }
    }

    void SwapPositions(bool swapped)
    {
        Vector3 translation = swapWorldTranslation;
        if (swapped) // move back up
            translation = -translation;

        followObjectWorldOffset.worldOffset += translation;
    }

    void SwapIKTargets(bool swapped)
    {
        if (swapped)
        {
            followObjectWorldOffset.target = swappedTarget;
        }
        else
        {
            followObjectWorldOffset.target = unswappedTarget;
        }
    }
}
