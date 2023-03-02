using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class HexGridGenerator : EditorWindow
{
    private Vector2 gridSize;
    private float spacing;
    private GameObject hexPrefab;

    [MenuItem("Lukens/Generators/Hex Grid Generator")]
    public static void ShowEditor()
    {
        GetWindow<HexGridGenerator>().titleContent = new GUIContent("Hex Grid Generator");
    }

    private void OnGUI()
    {
        hexPrefab = (GameObject)EditorGUILayout.ObjectField(hexPrefab, typeof(GameObject), false);
        gridSize = EditorGUILayout.Vector2Field("Size", gridSize);
        spacing = EditorGUILayout.FloatField("Spacing", spacing);

        if (GUILayout.Button("Generate Grid"))
        {
            GenerateGrid();
        }
    }

    private void GenerateGrid()
    {
        // horiz = 3/4 * width = 3/2 * size

        //float hexWidth = tileSize.x * 2;
        //float hexHeight = tileSize.y * Mathf.Sqrt(3)
        // r = s / (2 x cos(PI/6))

        float size = 0.04f;

        Debug.Log("Tile Size: " + size);

        float horzSpacing = size * Mathf.Sqrt(3) + spacing;
        float vertSpacing = size * 1.5f + spacing;
        float offset = Mathf.Sqrt(3) * size / 2;

        GameObject newParent = new GameObject("Hex Grid");

        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                float offsetAmount = 0;

                if (y % 2 != 0)
                    offsetAmount = offset;

                GameObject newTile = (GameObject)PrefabUtility.InstantiatePrefab(hexPrefab);

                newTile.transform.position = new Vector3(x * horzSpacing + offsetAmount, 0, y * vertSpacing);
                newTile.transform.parent = newParent.transform;
            }
        }

        Debug.Log("Grid Generated!");
    }
}
