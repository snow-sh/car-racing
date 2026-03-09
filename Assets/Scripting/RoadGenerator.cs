using UnityEngine;
using System.Collections.Generic;

public class RoadGenerator : MonoBehaviour
{
    [Header("Settings")]
    public GameObject[] roadPrefabs; 
    public Transform player;
    public GameObject[] aiCarPrefabs; 
    public int startPrefabIndex = 0; 

    [Header("Generation Logic")]
    private float spawnZ = 0;        
    private float roadLength = 499.9f; 
    private int roadsOnScreen = 4;  
    
    private List<GameObject> activeRoads = new List<GameObject>();
    private int currentPrefabIndex = 0;
    private int zoneLength = 0;

    void Start()
    {
        activeRoads.Clear();
        spawnZ = 0;
        currentPrefabIndex = startPrefabIndex;
        zoneLength = Random.Range(2, 4); 

        for (int i = 0; i < roadsOnScreen; i++)
        {
            SpawnRoad();
        }
    }

    void Update()
    {
        if (player == null) return;

        if (player.position.z > activeRoads[0].transform.position.z + roadLength)
        {
            SpawnRoad();
            DeleteOldRoad();
        }
    }


    void SpawnRoad()
    {
        if (zoneLength <= 0)
        {
            int nextIndex = currentPrefabIndex;
            while (nextIndex == currentPrefabIndex)
            {
                nextIndex = Random.Range(0, roadPrefabs.Length);
            }
            currentPrefabIndex = nextIndex;
            zoneLength = Random.Range(2, 4); 
        }

        GameObject road = Instantiate(roadPrefabs[currentPrefabIndex], new Vector3(0, 0, spawnZ), Quaternion.identity);
        activeRoads.Add(road);

        Transform[] allChildren = road.GetComponentsInChildren<Transform>();
        
        foreach (Transform child in allChildren)
        {
            if (child.name.Contains("AICarSpawnPoint"))
            {
                if (Random.value < 0.6f) 
                {
                    int randomCarIndex = Random.Range(0, aiCarPrefabs.Length);
                    
                    GameObject newAiCar = Instantiate(aiCarPrefabs[randomCarIndex], child.position, child.rotation);
                    
                    newAiCar.transform.SetParent(road.transform, true);
                    
                    newAiCar.name = "EnemyCar";
                }
            }
        }

        spawnZ += roadLength;
        zoneLength--;
    }

    void DeleteOldRoad()
    {
        if (activeRoads.Count > roadsOnScreen) 
        {
            GameObject objToDestroy = activeRoads[0];
            activeRoads.RemoveAt(0);
            Destroy(objToDestroy);
        }
    }
}