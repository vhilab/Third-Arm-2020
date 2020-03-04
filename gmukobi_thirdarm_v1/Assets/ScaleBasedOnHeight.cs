using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBasedOnHeight : MonoBehaviour
{
    [SerializeField] private Transform topTarget = default;
    [SerializeField] private Transform bottomTarget = default;
    [SerializeField] private float magicScaleMultiplier = 1.0f;

    public void Scale()
    {
        float heightDifference = topTarget.position.y - bottomTarget.position.y;
        float targetScale = heightDifference * magicScaleMultiplier;

        transform.localScale = new Vector3(targetScale, targetScale, targetScale);
    }
}
