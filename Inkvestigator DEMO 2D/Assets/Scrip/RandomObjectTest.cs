using System.Collections.Generic;
using UnityEngine;

public class RandomObjectTest : MonoBehaviour
{

    Vector2 randomPosition;
    [SerializeField] private GameObject _spawnObject; // can asign any prefab in the Unity inspector
    [SerializeField] private int _spawnAmount = 8; // amount of objects that will spawn
    [SerializeField] private float _xRange = 3f; // range for x axis
    [SerializeField] private float _yRange = 3f; // range for y axis
    [SerializeField] private float _minDistance = 1f;// min distance between objects

    // Stores positions of all sucessfully spawned objects
    private List<Vector2> _spawnedPositions = new List<Vector2>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int attempts = 0;
        int maxAttempts = 1000; //stops any infinite loops

        for (int objectIndex = 0; objectIndex < _spawnAmount; objectIndex++)
        {
            Vector2 randomPosition;
            bool positionFound = false;

            while (!positionFound && attempts < maxAttempts)
            {
                float xPosition = Random.Range(0 - _xRange, 0 + _xRange);
                float yPosition = Random.Range(0 - _yRange, 0 + _yRange);
                randomPosition = new Vector2(xPosition, yPosition);
                
                bool overlap = false;

                foreach (Vector2 existingPosition in _spawnedPositions)
                {
                    if (Vector2.Distance(randomPosition, existingPosition) < _minDistance)
                    {
                        overlap = true;
                        break;
                    }
                }

                if (!overlap)
                {
                    Instantiate(_spawnObject, randomPosition, Quaternion.identity);
                    _spawnedPositions.Add(randomPosition);
                    positionFound = true;
                    
                }

                attempts++;
            }

            if (attempts >= maxAttempts)
            {
                Debug.LogWarning("Max attempts reached. Some objects might have not spawned.");
                break;
            }

        }

        //transform.position = randomPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
