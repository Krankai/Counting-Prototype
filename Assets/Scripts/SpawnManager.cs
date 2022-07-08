using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Range(0, 30)]
    public int totalCount = 30;

    public float spawnCooldown = 0.5f;

    [SerializeField]
    GameObject spawnPosition;

    [SerializeField, Range(1f, 10f)]
    float spawnInterval = 2.5f;

    ObjectPooler pooler;

    Vector3 position;

    Vector3 vector3Zero = Vector3.zero;

    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        pooler = GetComponent<ObjectPooler>();
        position = spawnPosition.transform.position;

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void SpawnSingle()
    {
        bool canSpawn = SpawnPooledObject();
        if (canSpawn)
        {
            audioManager.PlaySpawnSound();
        }
        else
        {
            audioManager.PlaySpawnEmptySound();
        }
    }

    void StartSpawning()
    {
        InvokeRepeating("SpawnPooledObject", 0f, spawnInterval);
    }

    void StopSpawning()
    {
        CancelInvoke();
    }

    // Return true if can spawn object; otherwise, return false
    bool SpawnPooledObject()
    {
        if (totalCount <= 0)
        {
            return false;
        }

        var pooledObject = pooler.GetPooledObject();
        if (pooledObject != null)
        {
            pooledObject.transform.position = position;
            pooledObject.SetActive(true);

            --totalCount;

            return true;
        }

        return false;
    }

    public void RecollectDestroyedObject(GameObject objectToRecollect)
    {
        objectToRecollect.SetActive(false);

        Rigidbody rb = objectToRecollect.GetComponent<Rigidbody>();
        rb.velocity = rb.angularVelocity = vector3Zero;

        BallBehaviour script = objectToRecollect.GetComponent<BallBehaviour>();
        script.CurrentColor = BoxColor.None;

        ++totalCount;
    }
}
