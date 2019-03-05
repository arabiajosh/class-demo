using System.Collections;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGenerator = (MapGenerator)target;

        if (DrawDefaultInspector())
        {
            if (mapGenerator.AutoUpdate)
            {
                mapGenerator.DrawEditorPreview();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            mapGenerator.DrawEditorPreview();
        }
    }
}
