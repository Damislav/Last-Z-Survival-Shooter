using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public GameObject ammoPrefab = null;

    public int PoolSize = 100;

    private GameObject[] AmmoArray;

    public static AmmoManager AmmoManagerSingleton = null;

    public Queue<Transform> AmmoQueue = new Queue<Transform>();

    private void Awake()
    {
        if (AmmoManagerSingleton != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        AmmoManagerSingleton = this;
    }

    void Start()
    {
        //specify array size
        AmmoArray = new GameObject[PoolSize];

        for (int i = 0; i < PoolSize; i++)
        {
            AmmoArray[i] = Instantiate(ammoPrefab, Vector3.zero, Quaternion.identity);
            Transform ObjTransform = AmmoArray[i].GetComponent<Transform>();
            ObjTransform.parent = transform;
            AmmoQueue.Enqueue(ObjTransform);
            AmmoArray[i].SetActive(false);
        }
    }


    public static Transform SpawnAmmo(Vector3 Position, Quaternion Rotation)
    {
        Transform SpawnedAmmo = AmmoManagerSingleton.AmmoQueue.Dequeue();
        SpawnedAmmo.gameObject.SetActive(true);
        SpawnedAmmo.position = Position;
        SpawnedAmmo.rotation = Rotation;

        AmmoManagerSingleton.AmmoQueue.Enqueue(SpawnedAmmo);

        return SpawnedAmmo;
    }


}