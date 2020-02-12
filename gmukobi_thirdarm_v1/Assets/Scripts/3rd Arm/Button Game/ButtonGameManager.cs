using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonGameManager : MonoBehaviour
{
    [SerializeField] private TouchableButton button3;
    [SerializeField] private TouchableButton buttonL;
    [SerializeField] private TouchableButton buttonR;

    public UnityEvent OnAllButtonsPushed;

    private MaterialEmissionChanger button3EmissionChanger;

    private void Start()
    {
        button3EmissionChanger = button3.gameObject.GetComponentInChildren<MaterialEmissionChanger>();
    }

    private void OnEnable()
    {
        button3.OnButtonTouchStateChange.AddListener(CheckButtonGameState);
        buttonL.OnButtonTouchStateChange.AddListener(CheckButtonGameState);
        buttonR.OnButtonTouchStateChange.AddListener(CheckButtonGameState);
    }

    private void OnDisable()
    {
        button3.OnButtonTouchStateChange.RemoveListener(CheckButtonGameState);
        buttonL.OnButtonTouchStateChange.RemoveListener(CheckButtonGameState);
        buttonR.OnButtonTouchStateChange.RemoveListener(CheckButtonGameState);
    }

    void CheckButtonGameState()
    {
        // make 3rd button light up if both arm buttons are lit up
        if (buttonL.isBeingTouched && buttonR.isBeingTouched)
        {
            button3EmissionChanger.SetEmission(true);
        }
        else
        {
            button3EmissionChanger.SetEmission(false);
        }

        if (buttonL.isBeingTouched && buttonR.isBeingTouched && button3.isBeingTouched)
        {
            OnAllButtonsPushed.Invoke();
        }
    }
}
