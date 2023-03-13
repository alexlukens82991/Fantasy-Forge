using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTerrainState", menuName = "Hex Grid/Terrain State")]
public class TerrainState : ScriptableObject
{
    public List<TileState> TileStates;
}
