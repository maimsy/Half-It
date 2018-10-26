using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Halfit.Generation;

public class GenerateMesh : MonoBehaviour
{
    [SerializeField] private Vector2[] _vertices = {
        new Vector2(0,0),
        new Vector2(0,1),
        new Vector2(1,1),
        new Vector2(1,0)
    };

    private void OnValidate()
    {
        UpdateMesh();
    }

    private void UpdateMesh()
    {
        // Use the triangulator to get indices for creating triangles
        Triangulator tr = new Triangulator(_vertices);
        int[] indices = tr.Triangulate();

        // Create the Vector3 vertices
        Vector3[] vertices = new Vector3[_vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(_vertices[i].x, _vertices[i].y, 0);
        }

        // Create the mesh
        Mesh msh = new Mesh();
        msh.vertices = vertices;
        msh.triangles = indices;
        msh.RecalculateNormals();
        msh.RecalculateBounds();

        // Set up game object with mesh;
        MeshFilter filter = GetComponent<MeshFilter>();
        filter.mesh = msh;
    }

    public void Generate()
    {
        gameObject.SetActive(true);
        UpdateMesh();
    }
}
