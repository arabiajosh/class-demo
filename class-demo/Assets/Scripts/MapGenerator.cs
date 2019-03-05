using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    public enum drawMode
    {
        NoiseMap, ColorMap, Mesh
    }

    public drawMode Mode;

    public const int ChunkEdgeToEdge = 241;

    [Range(0, 6)]
    public int EditorLevelOfDetail = 0;

    public float NoiseScale = 25;

    public int Octaves = 5;

    [Range(0f, 1f)]
    public float Persist = .5f;
    public float Lacun = 2f;

    public int Seed = 0;
    public Vector2 SeedOffset = new Vector2(0f, 0f);

    public float MeshHeightMultiplier = 1;
    public AnimationCurve MeshHeightCurve;

    public TerrainType[] Terrains;

    public bool AutoUpdate = false;


    MappingData GenerateMappingData(Vector2 Center)
    {
        float[,] NoiseData = Noise.MakeSomeNoise(ChunkEdgeToEdge, ChunkEdgeToEdge, Seed, NoiseScale, Octaves, Persist, Lacun, Center + SeedOffset);
        Color[] ColorData = new Color[ChunkEdgeToEdge * ChunkEdgeToEdge];

        for(int y = 0; y < ChunkEdgeToEdge; y++)
        {
            for(int x = 0; x < ChunkEdgeToEdge; x++)
            {
                float HeightAtCoords = NoiseData[x, y];
                for(int ColorIndex = 0; ColorIndex < Terrains.Length; ColorIndex++)
                {
                    
                    if(HeightAtCoords <= Terrains[ColorIndex].TerrainHeight)
                    {
                        ColorData[y * ChunkEdgeToEdge + x] = Terrains[ColorIndex].TerrainColor;
                        break;
                    }
                }
            }
        }

        return new MappingData(NoiseData, ColorData);
    }

    public void DrawEditorPreview()
    {
        MappingData md = GenerateMappingData(Vector2.zero);

        MapDisplay display = FindObjectOfType<MapDisplay>();

        if(Mode == drawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(md.NoiseData));
            display.ClearMesh();

        } else if(Mode == drawMode.ColorMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(md.ColorData, ChunkEdgeToEdge, ChunkEdgeToEdge));
            display.ClearMesh(); 
        } else if(Mode == drawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(md.NoiseData, MeshHeightMultiplier, MeshHeightCurve, EditorLevelOfDetail), TextureGenerator.TextureFromColorMap(md.ColorData, ChunkEdgeToEdge, ChunkEdgeToEdge));
        }
    }

    private void OnValidate()
    {
        if (Lacun < 1) Lacun = 1;
        if (Octaves < 0) Octaves = 0;
    }
}

[System.Serializable]
public struct TerrainType
{
    public string TerrainName;
    public float TerrainHeight;
    public Color TerrainColor;
}

public struct MappingData
{
    public readonly float[,] NoiseData;
    public readonly Color[] ColorData;

    public MappingData(float[,] noiseData, Color[] colorData)
    {
        NoiseData = noiseData;
        ColorData = colorData;
    }
}