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
        //Debug.Log($"Collided with layer {other.gameObject.layer}");
        if (((1 << other.gameObject.layer) & collisionLayerMask) != 0)
        {
            //Debug.Log($"Layer matches collision mask!");
            OnButtonTouched.Invoke();
        }
    }
}
