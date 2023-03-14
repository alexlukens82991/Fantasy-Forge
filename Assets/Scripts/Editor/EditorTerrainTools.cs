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
        //hexTerrainController.EditorSetState(terrainState);
    }

    //public void EditorSetState(TerrainState terrainState)
    //{
    //    foreach (HexTile hexTile in HexTiles)
    //    {
    //        string[] expanded = hexTile.gameObject.name.Split('_');
    //        hexTile.SetTileState(terrainState.TileStates[int.Parse(expanded[1])], false);
    //    }

    //    foreach (Transform child in m_BuildingsParent)
    //    {
    //        Destroy(child.gameObject);
    //    }


    //    foreach (BuildingState buildingState in terrainState.BuildingStates)
    //    {
    //        GameObject newBuilding = Instantiate(m_GenericBuildingPrefab, m_BuildingsParent);

    //        newBuilding.transform.position = buildingState.Position;
    //        newBuilding.transform.rotation = buildingState.Rotation;
    //        newBuilding.transform.localScale = buildingState.Scale;
    //    }
    //}

    //public void SaveCurrentTerrainState()
    //{
    //    List<TileState> tileStates = new();

    //    foreach (HexTile tile in HexTiles)
    //    {
    //        tileStates.Add(tile.TileState);
    //    }

    //    CurrentTerrainState.TileStates = tileStates;

    //    List<BuildingState> buildingStates = new();

    //    foreach (Transform child in m_BuildingsParent)
    //    {
    //        BuildingState newState = new(child);
    //        buildingStates.Add(newState);
    //    }

    //    CurrentTerrainState.BuildingStates = buildingStates;

    //    // TODO: MUST MOVE TO EDITOR SCRIPT
    //    EditorUtility.SetDirty(CurrentTerrainState);
    //}
}
