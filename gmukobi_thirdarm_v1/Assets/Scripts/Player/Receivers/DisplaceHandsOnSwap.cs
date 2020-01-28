/* =============================================================================
 * Purpose: swap the target of the FollowObjectWorldOffset component with an
 * alternative swappedTarget, allowing for the swapping of hands and feet by
 * changing which transform the target GameObjects follow.
 * 
 * Author: Gabriel Mukobi
 * ============================================================================= */

using UnityEngine;

public class DisplaceHandsOnSwap : MonoBehaviour
{
    public Vector3 displacement;
    FollowObjectWorldOffset followObjectWorldOffset;
    PlayerKeyboardInputDispatcher playerKeyboardInputDispatcher;

    // Start is called before the first frame update
    void Start()
    {
        followObjectWorldOffset = GetComponent<FollowObjectWorldOffset>();

        playerKeyboardInputDispatcher = GetComponentInParent<PlayerKeyboardInputDispatcher>();
        playerKeyboardInputDispatcher.OnSwapButtonPress += DisplaceFollower;
    }

    private void OnDestroy()
    {
        playerKeyboardInputDispatcher.OnSwapButtonPress -= DisplaceFollower;
    }

    void DisplaceFollower(bool swapped)
    { 
        if (swapped)
        {
            // swap stuff
            SwapPositions(swapped);
        }
        else
        {
            // reset to original value by unswapping
            SwapPositions(swapped);
        }
    }

    void SwapPositions(bool swapped)
    {
        Vector3 translation = displacement;
        if (!swapped) // move back up
            translation = -translation;

        followObjectWorldOffset.worldOffset += translation;
    }
}
