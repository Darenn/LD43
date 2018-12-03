using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{

    public Transform SpawnPosition;
    public GameObject[] PrefabsToSpawn;
    public float SpawnTime;

    private GameObject Spawned;
    private bool spawning = false;

    void Start()
    {
        spawnObject();
    }

    void Update()
    {
        if (Spawned != null && Spawned.GetComponent<Food>().isGrabbed && !spawning)
        {
            Spawned = null;
            StartCoroutine(WaitAndSpawn());
        }
    }

    IEnumerator WaitAndSpawn()
    {
        spawning = true;
        yield return new WaitForSeconds(SpawnTime);
        spawnObject();
        spawning = false;
    }

    void spawnObject()
    {
        Spawned = Instantiate(PrefabsToSpawn[Random.Range(0, PrefabsToSpawn.Length)], SpawnPosition.transform);
        Spawned.transform.localPosition = Vector3.zero;
    }
}
