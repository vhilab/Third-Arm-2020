﻿using UnityEngine;
public enum ThirdArmBuildType
{
    LabDemo,
    TribecaFilm
}

[CreateAssetMenu(fileName = "ThirdArmSettings", menuName = "ScriptableObjects/ThirdArmSettingsScriptableObject", order = 1)]
public class ThirdArmSettingsScriptableObject : ScriptableObject
{
    public ThirdArmBuildType thirdArmBuildType { get; private set; }
    public float growAngleThresholdHMDEulerX;
}
