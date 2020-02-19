using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonGameManager : MonoBehaviour
{
    [SerializeField] private TouchableButton button3 = default;
    [SerializeField] private TouchableButton buttonL = default;
    [SerializeField] private TouchableButton buttonR = default;

    public UnityEvent OnAllButtonsPushed;

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
        if (buttonL.isBeingTouched && buttonR.isBeingTouched && button3.isBeingTouched)
        {
            OnAllButtonsPushed.Invoke();
        }
    }
}
