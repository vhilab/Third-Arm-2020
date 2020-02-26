using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SteamVRHapticTriggerer : MonoBehaviour
{
    public SteamVR_Action_Vibration hapticAction;
    public ushort defaultMicroSecondsDuration;

    public void TriggerHapticPulse(ushort microSecondsDuration)
    {
        float seconds = (float)microSecondsDuration / 1000000f;
        hapticAction.Execute(0, seconds, 1f / seconds, 1, SteamVR_Input_Sources.Any);
    }
    public void TriggerHapticPulseDefault()
    {
        TriggerHapticPulse(defaultMicroSecondsDuration);
    }
}
