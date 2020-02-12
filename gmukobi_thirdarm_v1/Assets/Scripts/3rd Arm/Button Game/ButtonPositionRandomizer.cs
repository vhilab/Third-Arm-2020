using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPositionRandomizer : MonoBehaviour
{
    [SerializeField] private Transform button = default;
    [Tooltip("X range to spawn in on either side of my transform.")]
    [SerializeField] private float spawnRangeX = default;
    [Tooltip("Y range to spawn in on either side of my transform.")]
    [SerializeField] private float spawnRangeY = default;
    [SerializeField] private float minNewSpawnDistance = default;

    public void RandomizeButtonPositionMinDistance()
    {
        Vector3 oldPosition = button.position;
        for (int i = 0; i < 5; i++) // try several times then fail
        {
            RandomizeButtonPosition();
            if (Vector3.Distance(button.position, oldPosition) >= minNewSpawnDistance) return;
        }
    }

    private void RandomizeButtonPosition()
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
