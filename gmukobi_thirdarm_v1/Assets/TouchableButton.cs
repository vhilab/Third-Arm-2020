using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchableButton : MonoBehaviour
{
    [SerializeField] private LayerMask collisionLayerMask = default;
    [SerializeField] private GameObject particlePrefab = default;

    public UnityEvent OnButtonTouched;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"Collided with layer {other.gameObject.layer}");
        if (((1 << other.gameObject.layer) & collisionLayerMask) != 0)
        {
            //Debug.Log($"Layer matches collision mask!");
            if (particlePrefab != null)
            {
                GameObject particleObject = Instantiate(particlePrefab, transform.position, transform.rotation) as GameObject;
                particleObject.GetComponent<ParticleSystem>().Play();
                Destroy(particleObject, 2f);
            }

            OnButtonTouched.Invoke();
        }
    }
}
