using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActiveGameObjects : MonoBehaviour
{
    [Tooltip("The objects you want to switch the active state of. " +
        "By default, the first element is the only one active")]
    public GameObject[] toSwitch;

    private int activeIndex = 0;

    private void Start()
    {
        // activate only the first element
        foreach(GameObject go in toSwitch)
        {
            go.SetActive(false);
        }
        toSwitch[activeIndex].SetActive(true);
    }

    public void ToggleActiveObject()
    {
        // switches between which one of all GameObjects in toSwitch are active
        toSwitch[activeIndex].SetActive(false); // set previously active object inactive
        activeIndex = (activeIndex + 1) % toSwitch.Length; // find next index 
        toSwitch[activeIndex].SetActive(true); // set next to active
    }
}
