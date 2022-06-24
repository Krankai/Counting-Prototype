using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;

    [SerializeField]
    GameObject objectToPool;

    [SerializeField]
    int amountToPool;

    List<GameObject> pooledObjects;

    void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tempObject;

        for (int i = 0; i < amountToPool; ++i)
        {
            tempObject = Instantiate(objectToPool);
            tempObject.SetActive(false);
            pooledObjects.Add(tempObject);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; ++i)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
