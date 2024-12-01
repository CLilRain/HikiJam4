using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCloudPool : MonoBehaviour
{
    public PlatformCloud prefab;

    Queue<PlatformCloud> pool;

    const int PoolSize = 1;

    void Start()
    {
        pool = new Queue<PlatformCloud>(PoolSize);

        for (int i = 0; i < PoolSize; i++)
        {
            InstantiateCloud();
        }
    }

    PlatformCloud InstantiateCloud()
    {
        var cloud = Instantiate(prefab, parent: transform);
        cloud.Initialize(this);
        cloud.gameObject.SetActive(false);
        pool.Enqueue(cloud);

        return cloud;
    }

    public void Spawn(Vector3 position)
    {
        PlatformCloud cloud;

        if (pool.Count <= 0)
        {
            cloud = InstantiateCloud();
        }
        else
        {
            cloud = pool.Dequeue();
        }

        cloud.transform.position = position;
        Debug.DrawLine(Vector3.zero, position, Color.red, 5f);
        cloud.gameObject.SetActive(true);
        Debug.Log("spawned cloud");
        cloud.Show(position);
    }

    public void ReturnToPool (PlatformCloud cloud)
    {
        cloud.gameObject.SetActive(false);
        pool.Enqueue(cloud);
    }
}