using UnityEngine;
using System.Collections.Generic;

public class RoadGenerator : MonoBehaviour
{
    public GameObject[] roadPrefabs; 
    public Transform player;         
    private float spawnZ = 0;        
    private float roadLength = 100;  
    private int roadsOnScreen = 5;
    private List<GameObject> activeRoads = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < roadsOnScreen; i++)
        {
            SpawnRoad(Random.Range(0, roadPrefabs.Length));
        }
    }

    void Update()
    {
        // If the player is getting close to the end of the current road
        if (player.position.z > spawnZ - (roadsOnScreen * roadLength))
        {
            SpawnRoad(Random.Range(0, roadPrefabs.Length));
            DeleteOldRoad();
        }
    }

    void SpawnRoad(int index)
    {
        GameObject road = Instantiate(roadPrefabs[index], transform.forward * spawnZ, Quaternion.identity);
        activeRoads.Add(road);
        spawnZ += roadLength;
    }

    void DeleteOldRoad()
    {
        Destroy(activeRoads[0]);
        activeRoads.RemoveAt(0);
    }
}