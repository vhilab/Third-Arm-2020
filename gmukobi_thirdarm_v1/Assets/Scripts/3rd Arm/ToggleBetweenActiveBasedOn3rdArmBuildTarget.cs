using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBetweenActiveBasedOn3rdArmBuildTarget : MonoBehaviour
{
    public GameObject[] ObjectsToEnableForLabDemo;
    public GameObject[] ObjectsToEnableForFilmProject;

    private void Start()
    {
        bool isLabDemo = ThirdArmSettingsReader.Instance.settings.thirdArmBuildType == ThirdArmBuildType.LabDemo;
        foreach (GameObject gameObject in ObjectsToEnableForLabDemo)
        {
            gameObject.SetActive(isLabDemo);
        }
        foreach (GameObject gameObject in ObjectsToEnableForFilmProject)
        {
            gameObject.SetActive(!isLabDemo);
        }
    }
}
