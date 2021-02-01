using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridGenerator : MonoBehaviour
{
    Tilemap groundTiles;
    public GameObject gridSquareCenter;
    public GameObject gridSquareBottomCenter;

    public Transform holder;

    public int width;
    public int height;
    private void Start()
    {
        groundTiles = GetComponent<Tilemap>();
        groundTiles.CompressBounds();

        int minWidth = -width + (int)transform.position.x;
        int minHeight = -height+ (int)transform.position.y;

        int maxWidth = width + (int)transform.position.x;
        int maxHeight = height + (int)transform.position.y;

        for (int y = minHeight; y < maxHeight; y+=2)
        {
            for (int x = minWidth; x < maxWidth; x+=2)
            {
                if (y == minHeight)
                    Instantiate(gridSquareBottomCenter, new Vector2(x + (groundTiles.cellSize.x), y + (groundTiles.cellSize.y)), Quaternion.identity, holder);
                else
                    Instantiate(gridSquareCenter, new Vector2(x + (groundTiles.cellSize.x), y + (groundTiles.cellSize.y)), Quaternion.identity, holder);
            }
        }
    }

}
