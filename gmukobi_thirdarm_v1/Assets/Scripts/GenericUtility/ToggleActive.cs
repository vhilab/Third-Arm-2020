using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActive : MonoBehaviour
{
    [SerializeField] private bool defaultActive = false;

    private void Start()
    {
        gameObject.SetActive(defaultActive);
    }

    public void ToggleGameObjectActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
