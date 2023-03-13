using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HexTerrainController : MonoBehaviour
{
    public TerrainState CurrentTerrainState;
    public TerrainState[] m_TestableStates;
    private int currState;
    public List<HexTile> HexTiles;

    private void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            LoadTerrainState(m_TestableStates[currState]);
            currState++;

            if (currState == m_TestableStates.Length)
                currState = 0;
        }
    }

    public void LoadTerrainState(TerrainState terrainState)
    {
        foreach (HexTile hexTile in HexTiles)
        {
            string[] expanded = hexTile.gameObject.name.Split('_');
            hexTile.SetTileState(terrainState.TileStates[int.Parse(expanded[1])]);
        }
    }

    public void SaveCurrentTerrainState()
    {
        print("SAVING TERRAIN STATE");
        List<TileState> tileStates = new();

        foreach (HexTile tile in HexTiles)
        {
            tileStates.Add(tile.TileState);
        }

        CurrentTerrainState.TileStates = tileStates;

        // TODO: MUST MOVE TO EDITOR SCRIPT
        EditorUtility.SetDirty(CurrentTerrainState);
    }
}
