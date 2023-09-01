using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject BallBasketPrefab;
   // [SerializeField] Vector3 desiredPosition = Vector3.zero;
    private ShapeGenrator shapeGenrator;
    public float DesiredXpos = 1.89f;
    public float spacing = 0.6f;

    [Header("Bet Spawner")]
    public float minX;
    public float maxX;
    public float spawnInterval;
    public GameObject betBallprefab;
    private void Start()
    {
        shapeGenrator = FindObjectOfType<ShapeGenrator>();
       // PositionPattern();
        for (int i = 0; i < shapeGenrator.rowCount; i++)
        {
            float x = spacing * i ;
            GameObject basketPrefab = Instantiate(BallBasketPrefab, new Vector3(x + DesiredXpos, -3,0), Quaternion.identity);
            basketPrefab.transform.SetParent(transform);
        }
        StartCoroutine(SpawnCoroutine());

    }

  
    /*    void PositionPattern()
        {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(desiredPosition);
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
            transform.position = worldPoint;
        }
    */
    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            // Calculate a random position within the given X range
            float randomX = Random.Range(minX, maxX);

            // Instantiate the objectPrefab at the calculated position
            Instantiate(betBallprefab, new Vector3(randomX, transform.position.y, transform.position.z), Quaternion.identity);

            // Wait for the spawnInterval before spawning the next object
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
