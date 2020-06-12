using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pooler : MonoBehaviour
{


    public int startAmount = 10;
    public List<GameObject> pool;
    [SerializeField] private GameObject parentObject;
    [SerializeField] private GameObject pooledObject;

    public GameObject PooledObject
    {
        get
        {
            return pooledObject;
        }
        set
        {
            pooledObject = value;
            //if(pool != null && pool.Count > 0)
            //{
            //    for (int i = 0; i < pool.Count; i++)
            //    {
            //        Destroy(pool[i]);
            //    }
            //}
            pool = new List<GameObject>();
            if (ParentObject == null)
            {
                ParentObject = new GameObject(pooledObject.name + " Pool");
                ParentObject.transform.parent = this.transform;
                ParentObject.transform.localPosition = Vector3.zero;
            }
            GameObject newObj;
            for (int i = 0; i < startAmount; i++)
            {
                newObj = Instantiate(PooledObject, ParentObject.transform);
                newObj.SetActive(false);
                pool.Add(newObj);
            }

        }
    }

    public GameObject ParentObject
    {
        get
        {
            return parentObject;
        }

        set
        {
            parentObject = value;
        }
    }

    private void Awake()
    {
        if(pooledObject != null)
        {
            PooledObject = pooledObject;
        }
    }
    public GameObject Get(bool getActivated = true)
    {
        if (pooledObject == null) return null;

        if (pool == null) pool = new List<GameObject>();
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeSelf)
            {
                pool[i].SetActive(getActivated);
                return pool[i];
            }
        }

        if (ParentObject == null)
        {
            Debug.Log("New oppa");
            ParentObject = new GameObject(PooledObject.name + " Pool");
            ParentObject.transform.parent = this.transform;
            ParentObject.transform.localPosition = Vector3.zero;
        }

        GameObject newObj = Instantiate(PooledObject, ParentObject.transform);
        newObj.SetActive(getActivated);
        pool.Add(newObj);
        return newObj;
    }

    public void SetAllOff()
    {
        if(pool != null)
        {
            for (int i = 0; i < pool.Count; i++)
            {
                pool[i].SetActive(false);
            }
        }
    }

}