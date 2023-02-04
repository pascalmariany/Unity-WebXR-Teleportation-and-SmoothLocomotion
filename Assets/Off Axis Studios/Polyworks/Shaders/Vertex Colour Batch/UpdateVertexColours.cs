using UnityEngine;
using System.Collections;
using UnityEditor;

namespace OffAxisStudios
{
    [ExecuteInEditMode]
    [System.Serializable]
    public class UpdateVertexColours : MonoBehaviour
    {
        public Material VertexMaterial;
        public bool UpdateColors = true;
        public Color[] NewColors;

        void Awake()
        {
            if (Application.isPlaying)
            {
                enabled = false;
            }

            Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
            var c1 = mesh.subMeshCount;
            var c2 = 0;
            if (NewColors != null)
            {
                c2 = NewColors.Length;
            }
            if (c1 != c2)
            {
                NewColors = new Color[mesh.subMeshCount];
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    NewColors[i] = GetComponent<Renderer>().sharedMaterials[i].color;
                }
            }
        }

        void Update()
        {
            if (UpdateColors)
            {
                if (GetComponent<Renderer>().sharedMaterials[0] != VertexMaterial)
                {
                    Material[] newmats = new Material[GetComponent<Renderer>().sharedMaterials.Length];

                    for (int i = 0; i < GetComponent<Renderer>().sharedMaterials.Length; i++)
                    {
                        newmats[i] = VertexMaterial;
                    }

                    GetComponent<Renderer>().sharedMaterials = newmats;
                }

                Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
                Color[] colors = new Color[mesh.vertices.Length];
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] t = mesh.GetTriangles(i);
                    for (int j = 0; j < t.Length; j++)
                    {
                        colors[t[j]] = NewColors[i];
                    }
                }
                mesh.colors = colors;
            }
        }
    }
}