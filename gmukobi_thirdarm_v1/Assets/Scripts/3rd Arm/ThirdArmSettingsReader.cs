using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdArmSettingsReader : MonoBehaviour
{
    public ThirdArmSettingsScriptableObject settings;
    
    public static ThirdArmSettingsReader Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(this.gameObject);
            throw new System.Exception("There shall only be one 3rd arm settings instance to rule them all");
        }
        Instance = this;
    }
}
