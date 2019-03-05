using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator
{
    public static Texture2D TextureFromColorMap(Color[] colors, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colors);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] heights)
    {
        int mapWidth = heights.GetLength(0);
        int mapHeight = heights.GetLength(1);

        Texture2D text = new Texture2D(mapWidth, mapHeight);

        Color[] colorMap = new Color[mapHeight * mapWidth];

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                colorMap[y * mapWidth + x] = Color.Lerp(Color.black, Color.white, heights[x, y]);
            }
        }

        return TextureFromColorMap(colorMap, mapWidth, mapHeight);
    }
}

