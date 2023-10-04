using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool instance;
    [SerializeField] GameObject prefab;
    [SerializeField][Range(0, 50)] int poolSize = 15;
    [SerializeField] bool canExpand = true;
    private List<GameObject> pooledObjects;

    public static ObjectPool Instance { get ; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }
    }


    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        if (!canExpand)
        {
            return null;
        }

        return CreateNewObject();
    }

    private GameObject CreateNewObject()
    {
        GameObject newObject = Instantiate(prefab);
        newObject.SetActive(false);
        pooledObjects.Add(newObject);
        return newObject;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        if (pooledObjects.Contains(obj))
        {
            obj.SetActive(false);
        }
    }
}
