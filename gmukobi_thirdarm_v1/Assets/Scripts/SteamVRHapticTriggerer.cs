using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SteamVRHapticTriggerer : MonoBehaviour
{
    public SteamVR_Action_Vibration hapticAction;
    public bool simpleHaptics;
    [Header("Simple vibration")]
    public ushort defaultMicroSecondsDuration;
    [Header("Full control")]
    public float durationSeconds;
    [Range(0,320)]
    public float frequency;
    [Range(0,1)]
    public float amplitude;

    public void TriggerHapticPulse(ushort microSecondsDuration)
    {
        float seconds = (float)microSecondsDuration / 1000000f;
        hapticAction.Execute(0, seconds, 1f / seconds, 1, SteamVR_Input_Sources.Any);
    }

    public void TriggerHapticPulse(float duration, float frequency, float amplitude)
    {
        hapticAction.Execute(0, duration, frequency, amplitude, SteamVR_Input_Sources.Any);
    }

    [ContextMenu("Trigger Vibration")]
    public void TriggerHapticPulseDefault()
    {
        if (simpleHaptics)
            TriggerHapticPulse(defaultMicroSecondsDuration);
        else
            TriggerHapticPulse(durationSeconds, frequency, amplitude);
    }
}
