using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject spawnPosition;

    [SerializeField, Range(1f, 10f)]
    float spawnInterval = 2.5f;

    ObjectPooler pooler;
    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        pooler = GetComponent<ObjectPooler>();
        position = spawnPosition.transform.position;

        StartSpawning();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartSpawning()
    {
        InvokeRepeating("SpawnPooledObject", 0f, spawnInterval);
    }

    void StopSpawning()
    {
        CancelInvoke();
    }

    void SpawnPooledObject()
    {
        var pooledObject = pooler.GetPooledObject();
        if (pooledObject != null)
        {
            pooledObject.transform.position = position;
            pooledObject.SetActive(true);
        }
    }
}
