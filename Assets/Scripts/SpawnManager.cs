using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField, Range(0, 30)]
    int totalCount = 30;

    public float spawnCooldown = 0.5f;

    [SerializeField]
    GameObject spawnPosition;

    [SerializeField, Range(1f, 10f)]
    float spawnInterval = 2.5f;

    ObjectPooler pooler;
    Vector3 position;

    Vector3 vector3Zero = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        pooler = GetComponent<ObjectPooler>();
        position = spawnPosition.transform.position;

        //StartSpawning();
    }

    public void SpawnSingle()
    {
        SpawnPooledObject();
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
        if (totalCount <= 0)
        {
            return;
        }

        var pooledObject = pooler.GetPooledObject();
        if (pooledObject != null)
        {
            pooledObject.transform.position = position;
            pooledObject.SetActive(true);

            --totalCount;
        }
    }

    // Recollect destroyed objects back to the total allocated
    public void OnRecollectObject(GameObject objectToRecollect)
    {
        objectToRecollect.SetActive(false);

        Rigidbody rb = objectToRecollect.GetComponent<Rigidbody>();
        rb.velocity = rb.angularVelocity = vector3Zero;

        ++totalCount;
    }
}
