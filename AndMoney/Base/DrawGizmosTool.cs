
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AndMoney {

    public static class DrawGizmosTool {
        public static void RectangleLine(Vector3 position, float cellSize, Color color) {
            Gizmos.color = color;
            float halfCellSize = cellSize / 2f;
            Vector3[] vertices =
            {
                new Vector3(halfCellSize, 0f, halfCellSize),
                new Vector3(-halfCellSize, 0f, halfCellSize),
                new Vector3(-halfCellSize, 0f, -halfCellSize),
                new Vector3(halfCellSize, 0f, -halfCellSize),
            };
            for (int i = 0; i < 4; i++) {
                Vector3 start = position + vertices[i];
                Vector3 end = position + vertices[(i + 1) % 4];
                Gizmos.DrawLine(start, end);
            }
        }
        public static void RectangleMesh(Vector3 position, float cellSize, Color color) {
            Gizmos.color = color;
            float halfCellSize = cellSize / 2f;
            Vector3[] vertices =
            {
                new Vector3(halfCellSize, 0f, halfCellSize),
                new Vector3(-halfCellSize, 0f, halfCellSize),
                new Vector3(-halfCellSize, 0f, -halfCellSize),
                new Vector3(halfCellSize, 0f, -halfCellSize),
            };
            int[] triangles = new int[]
            {
                3,1,0,
                3,2,1
            };
            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            Gizmos.DrawMesh(mesh, position);
        }
        public static void HexagonLine(Vector3 position, float radius, Color color) {
            Gizmos.color = color;
            Vector3[] vertices = new Vector3[6];
            for (int i = 0; i < 6; i++) {
                float angle = Mathf.PI / 3f * i;
                float x = radius * Mathf.Cos(angle);
                float z = radius * Mathf.Sin(angle);
                vertices[i] = position + new Vector3(x, 0f, z);
            }
            for (int i = 0; i < 6; i++) {
                Vector3 start = position + vertices[i];
                Vector3 end = position + vertices[(i + 1) % 6];
                Gizmos.DrawLine(start, end);
            }
        }
        public static void HexagonMesh(Vector3 position, float radius, Color color) {
            Gizmos.color = color;
            Vector3[] vertices = new Vector3[6];
            for (int i = 0; i < 6; i++) {
                float angle = Mathf.PI / 3f * i;
                float x = radius * Mathf.Cos(angle);
                float z = radius * Mathf.Sin(angle);
                vertices[i] = position + new Vector3(x, 0f, z);
            }
            int[] triangles = new int[]
            {
                0, 2, 1,
                0, 3, 2,
                0, 4, 3,
                0, 5, 4
            };
            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            Gizmos.DrawMesh(mesh, position);
        }
    }
}