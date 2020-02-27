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
            return;
        }
        Instance = this;
    }
}
