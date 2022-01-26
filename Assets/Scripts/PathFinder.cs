using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private GameObject[] paths;
    private int pathIndex = 0;

    [SerializeField] private float speed = 2f;

    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(paths[pathIndex].transform.position, transform.position) < .1f)
        {
            pathIndex++;
            if (pathIndex >= paths.Length)
            {
                pathIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, paths[pathIndex].transform.position, Time.deltaTime * speed );
    }
}
