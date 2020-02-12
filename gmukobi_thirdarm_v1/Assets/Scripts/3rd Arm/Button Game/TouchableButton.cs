using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchableButton : MonoBehaviour
{
    [SerializeField] private LayerMask collisionLayerMask = default;
    [SerializeField] private GameObject particlePrefab = default;

    public UnityEvent OnButtonEntered;
    public UnityEvent OnButtonExited;
    [HideInInspector] public UnityEvent OnButtonTouchStateChange;

    public bool isBeingTouched { get; private set; }

    public void PlayParticleEffect()
    {
        if (particlePrefab != null)
        {
            GameObject particleObject = Instantiate(particlePrefab, transform.position, transform.rotation) as GameObject;
            particleObject.GetComponent<ParticleSystem>().Play();
            Destroy(particleObject, 2f);
        }
        else
        {
            Debug.LogWarning("Trying to play a particle effect, but no particle prefab set.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"Collided with layer {other.gameObject.layer}");
        if (((1 << other.gameObject.layer) & collisionLayerMask) != 0)
        {
            //Debug.Log($"Layer matches collision mask!");
            isBeingTouched = true;
            OnButtonEntered.Invoke();
            OnButtonTouchStateChange.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log($"Collided with layer {other.gameObject.layer}");
        if (((1 << other.gameObject.layer) & collisionLayerMask) != 0)
        {
            isBeingTouched = false;
            OnButtonExited.Invoke();
            OnButtonTouchStateChange.Invoke();
        }
    }
}
