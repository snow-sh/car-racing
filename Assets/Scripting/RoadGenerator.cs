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

    // void SpawnRoad(int index)
    // {
    //     // GameObject road = Instantiate(roadPrefabs[index], transform.forward * spawnZ, Quaternion.identity);
    //     GameObject road = Instantiate(roadPrefabs[index], new Vector3(0, 0, spawnZ), Quaternion.identity);
    //     activeRoads.Add(road);
    //     spawnZ += roadLength;
    // }

void SpawnRoad(int index)
{
    Vector3 spawnPos = new Vector3(0, 0, spawnZ);
    GameObject road = Instantiate(roadPrefabs[index], spawnPos, Quaternion.identity);
    
    // This is the "Nudge" code
    // It finds the Road Architect script on the new road and tells it to wake up
    RoadArchitect.Road roadScript = road.GetComponentInChildren<RoadArchitect.Road>();
    if (roadScript != null)
    {
        roadScript.UpdateRoad(); // This does the 0.03 to 0.04 change for you!
    }

    activeRoads.Add(road);
    spawnZ += roadLength;
}

    void DeleteOldRoad()
    {
        Destroy(activeRoads[0]);
        activeRoads.RemoveAt(0);
    }
}