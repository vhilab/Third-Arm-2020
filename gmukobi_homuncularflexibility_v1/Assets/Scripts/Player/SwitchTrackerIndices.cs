/* =============================================================================
 * Purpose: Switch the indices of two SteamVR_TrackedObjects on "F" keypress in
 * order to allow for switch the left/right feet.
 * 
 * Author: Gabriel Mukobi
 * ============================================================================= */

using UnityEngine;
using Valve.VR;

public class SwitchTrackerIndices : MonoBehaviour
{
    public SteamVR_TrackedObject tracker0;
    public SteamVR_TrackedObject tracker1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SteamVR_TrackedObject.EIndex temp = tracker0.index;
            tracker0.index = tracker1.index;
            tracker1.index = temp;
        }
    }
}
