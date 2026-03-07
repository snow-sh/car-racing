using UnityEngine;
using System.Collections.Generic;

public class RoadGenerator : MonoBehaviour
{
    public GameObject[] roadPrefabs; 
    public Transform player;         
    
    private float spawnZ = 0;        
    private float roadLength = 250;  
    private int roadsOnScreen = 4;
    private List<GameObject> activeRoads = new List<GameObject>();

    void Start()
    {
        // Initial spawn of the first set of roads
        for (int i = 0; i < roadsOnScreen; i++)
        {
            SpawnRoad(Random.Range(0, roadPrefabs.Length));
        }
    }

    void Update()
    {
        // Checks if player has passed a certain point to spawn the next tile
        // This triggers when the player is closer than (roadsOnScreen * roadLength) to the end
        if (player.position.z - roadLength > spawnZ - (roadsOnScreen * roadLength))
        {
            SpawnRoad(Random.Range(0, roadPrefabs.Length));
            DeleteOldRoad();
        }
    }

    void SpawnRoad(int index)
    {
        Vector3 spawnPos = new Vector3(0, 0, spawnZ);
        GameObject road = Instantiate(roadPrefabs[index], spawnPos, Quaternion.identity);
        
        activeRoads.Add(road);
        spawnZ += roadLength;
    }

    void DeleteOldRoad()
    {
        // Safety check to ensure the list isn't empty before trying to destroy
        if (activeRoads.Count > 0)
        {
            Destroy(activeRoads[0]);
            activeRoads.RemoveAt(0);
        }
    }
}