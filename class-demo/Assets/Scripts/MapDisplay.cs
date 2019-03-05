using UnityEngine;
using System.Collections;

public class MapDisplay : MonoBehaviour
{
    public Renderer tr;
    public MeshFilter mf;
    public MeshRenderer mr;

    public void DrawTexture(Texture2D text)
    {
        tr.sharedMaterial.mainTexture = text;
        tr.transform.localScale = new Vector3(text.width, 1,text.height);
    }

    public void DrawMesh(MeshData mesh, Texture2D text)
    {
        mf.sharedMesh = mesh.CreateMesh();
        mr.sharedMaterial.mainTexture = text;
    }

    public void ClearMesh()
    {
        mf.sharedMesh.Clear();
        mr.sharedMaterial.mainTexture = null;
    }

    public void ClearTexture()
    {
        tr.sharedMaterial.mainTexture = null;
    }

}
