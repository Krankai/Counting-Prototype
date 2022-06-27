using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    [SerializeField, Range(10f, 50f)]
    float rangeX = 10, rangeZ = 10;

    [SerializeField]
    float minY = -10;

    [SerializeField]
    float maxY = 50;

    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // X axis
        if (transform.position.x < -rangeX || transform.position.x > rangeX)
        {
            spawnManager.OnRecollectObject(gameObject);
        }

        // Y axis
        if (transform.position.y < minY || transform.position.y > maxY)
        {
            spawnManager.OnRecollectObject(gameObject);
        }

        // Z axis
        if (transform.position.z < -rangeZ || transform.position.z > rangeZ)
        {
            spawnManager.OnRecollectObject(gameObject);
        }
    }
}
