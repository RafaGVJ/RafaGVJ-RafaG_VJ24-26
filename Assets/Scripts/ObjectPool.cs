using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
   [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 10; 

    private List<GameObject> pool;

    void Start()
    {
        
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false); 
            pool.Add(obj);
        }
    }

    public GameObject GetObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy) 
            {
                obj.SetActive(true); 
                return obj;
            }
        }
        return null; 
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false); 
    }
}
