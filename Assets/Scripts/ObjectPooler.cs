using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    
    [SerializeField] private GameObject pooledObject;
    [SerializeField] private int poolSize;
    [SerializeField] private bool willGrow = false;
    private List<GameObject> pooledObjects;

    public int GetPoolSize() {
        return poolSize;
    }

	private void Awake () {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < GetPoolSize(); i++) {
            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
	}

    public GameObject GetPooledObject() {
        for (int i = 0; i < pooledObjects.Count; i++) {
            if (!pooledObjects[i].activeInHierarchy) {
                return pooledObjects[i];
            }
        }

        if (willGrow) {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            pooledObjects.Add(obj);
            return obj;
        }
        return null;
    }
}
