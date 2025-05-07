using UnityEngine;

public class RandomObjectTest : MonoBehaviour
{

    Vector2 randomPosition;
    [SerializeField] private float xRange = 3f;
    [SerializeField] private float yRange = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float xPosition = Random.Range(0 - xRange, 0 + xRange);
        float yPosition = Random.Range(0 - yRange, 0 + yRange);

        randomPosition = new Vector2(xPosition, yPosition);
        transform.position = randomPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
