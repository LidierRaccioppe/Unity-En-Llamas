using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    
    
    public int minPickups = 5; // Minimum number of pickups to spawn
    public int maxPickups = 10; // limite superior de pickups a spawnear
    
    public GameObject pickupPrefab;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("pisado");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("pisado");
        
    }
    
    
    
    void SpawnPickupsRandomly()
    {
        int numPickupsToSpawn = Random.Range(minPickups, maxPickups + 1);

        for (int i = 0; i < numPickupsToSpawn; i++)
        {
            // Random position within a certain range (you can adjust as needed)
            Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0.5f, Random.Range(-10f, 10f));

            Instantiate(pickupPrefab, spawnPosition, Quaternion.identity);
        }
    }
    
    public void SpawnPickup(float x, float y, float z)
    {
        // Usaran las coordenadas que se le den
        Vector3 spawnPosition = new Vector3(x, y, z);
        // Pro
        Instantiate(pickupPrefab, spawnPosition, Quaternion.identity);
    }
    
    
    
    
}
