using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchableButton : MonoBehaviour
{
    [SerializeField] LayerMask collisionLayerMask = default;

    public UnityEvent OnButtonTouched;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & collisionLayerMask) != 0)
        {
            OnButtonTouched.Invoke();
        }
    }
}
