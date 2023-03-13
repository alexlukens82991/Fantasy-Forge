using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HexIDCreator : EditorWindow
{
    private GameObject hexGrid;

    [MenuItem("Lukens/Generators/Generate Hex IDs")]
    public static void ShowEditor()
    {
        GetWindow<HexIDCreator>().titleContent = new GUIContent("Hex Grid Generator");
    }

    private void OnGUI()
    {
        hexGrid = (GameObject)EditorGUILayout.ObjectField(hexGrid, typeof(GameObject), true);

        if (GUILayout.Button("Generate IDs"))
        {
            GenerateIDs();
        }
    }

    private void GenerateIDs()
    {
        foreach (Transform child in hexGrid.transform)
        {
            child.GetComponent<HexTile>().TileID = child.GetSiblingIndex();
            child.gameObject.name = "Tile_" + child.GetSiblingIndex();
        }
    }
}
