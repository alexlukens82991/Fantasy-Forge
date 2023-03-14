using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorTerrainTools : EditorWindow
{
    private HexTerrainController hexTerrainController;
    private TerrainState targetTerrainState;

    [MenuItem("Lukens/Hex/Terrain Tools")]
    public static void ShowEditor()
    {
        GetWindow<EditorTerrainTools>().titleContent = new GUIContent("Hex Grid Generator");
    }

    private void OnGUI()
    {
        targetTerrainState = (TerrainState)EditorGUILayout.ObjectField(targetTerrainState, typeof(TerrainState), false);
        hexTerrainController = (HexTerrainController)EditorGUILayout.ObjectField(hexTerrainController, typeof(HexTerrainController), true);

        if (GUILayout.Button("Set Terrain to Target State"))
        {
            SetTerrainToState(targetTerrainState);
        }
    }

    private void SetTerrainToState(TerrainState terrainState)
    {
        hexTerrainController.EditorSetState(terrainState);
    }
}
