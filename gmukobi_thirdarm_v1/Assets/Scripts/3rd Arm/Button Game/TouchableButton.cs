using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchableButton : MonoBehaviour
{
    [SerializeField] private string collisionTagFilter = default;
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
        if (other.CompareTag(collisionTagFilter))
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
        if (other.CompareTag(collisionTagFilter))
        {
            isBeingTouched = false;
            OnButtonExited.Invoke();
            OnButtonTouchStateChange.Invoke();
        }
    }
}
