using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenrator : MonoBehaviour
{
    public GameObject trianglePrefab; 
    public int rowCount = 5;
    public float triangleSize = 1.0f;
    public float spacing = 0.1f; 
    [SerializeField] Vector3 desiredPosition = Vector3.zero;

    public void Generate()
    {
        CreateTrianglePattern();
        PositionPattern();
    }
    public void CreateTrianglePattern()
    {
        float halfWidth = rowCount * (triangleSize * 0.5f + spacing * 0.5f);
        float height = Mathf.Sqrt(3) * triangleSize;

        for (int row = 0; row < rowCount-2 ; row++)
        {
            int trianglesInRow = rowCount - row;

            for (int i = 0; i < trianglesInRow; i++)
            {
                float x = -halfWidth + (i * (triangleSize + spacing)) + (row * (triangleSize * 0.5f + spacing * 0.5f));
                float y = -halfWidth + (row * (height * 0.66f + spacing));

                GameObject triangle = Instantiate(trianglePrefab, new Vector3(x, y, 0), Quaternion.identity);
                triangle.transform.SetParent(transform);
            }
        }
    }


    public void PositionPattern()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(desiredPosition);
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
        transform.position = worldPoint;
    }
}