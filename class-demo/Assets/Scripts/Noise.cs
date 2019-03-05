using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] MakeSomeNoise(int Width, int Height, int seed, float Scale, int numOctaves, float persist, float lacun, Vector2 offset)
    {
        float[,] noiseMap = new float[Width, Height];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[numOctaves];
        for (int i = 0; i < numOctaves; i++)
        {
            float offX = prng.Next(-100000, 100000) + offset.x;
            float offY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offX, offY);
        }

        Scale = Mathf.Clamp(Scale, .0001f, Mathf.Infinity);

        float max = float.MinValue;
        float min = float.MaxValue;

        float halfWidth = Width / 2f;
        float halfHeight = Height / 2f;

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {

                float amp = 1;
                float freq = 1;
                float noiseHeight = 0;

                for (int i = 0; i < numOctaves; i++)
                {
                    float testX = (x - halfWidth) / Scale * freq + octaveOffsets[i].x;
                    float testY = (y - halfHeight) / Scale * freq + octaveOffsets[i].y;

                    float perlin = Mathf.PerlinNoise(testX, testY) * 2 - 1;
                    noiseHeight += perlin * amp;

                    amp *= persist;
                    freq *= lacun;
                }
                if (noiseHeight > max) max = noiseHeight;
                else if (noiseHeight < min) min = noiseHeight;

                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(min, max, noiseMap[x, y]);
            }
        }
        return noiseMap;
    }
}
