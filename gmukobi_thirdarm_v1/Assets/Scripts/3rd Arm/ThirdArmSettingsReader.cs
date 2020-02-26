using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdArmSettingsReader : MonoBehaviour
{
    public ThirdArmSettingsScriptableObject settings;
    
    public static ThirdArmSettingsReader Instance;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }
}
