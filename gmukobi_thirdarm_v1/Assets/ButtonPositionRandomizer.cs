using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPositionRandomizer : MonoBehaviour
{
    [SerializeField] private Transform button;
    [Tooltip("X range to spawn in on either side of my transform.")]
    [SerializeField] private float spawnRangeX;
    [Tooltip("Y range to spawn in on either side of my transform.")]
    [SerializeField] private float spawnRangeY;

    private void Start()
    {
        RandomizeButtonPosition();
    }

    public void RandomizeButtonPosition()
    {
        float xDisplacement = Random.Range(-spawnRangeX, spawnRangeX);
        float yDisplacement = Random.Range(-spawnRangeY, spawnRangeY);
        button.position = transform.position + new Vector3(xDisplacement, yDisplacement, 0);
    }

    private void OnDrawGizmos()
    {
        // visualize the spawn range so you can see where them buttons gonna spawn

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(2 * spawnRangeX, 2 * spawnRangeY, 0));
    }
}
