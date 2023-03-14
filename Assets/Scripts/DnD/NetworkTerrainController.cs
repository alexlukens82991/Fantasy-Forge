using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkTerrainController : NetworkBehaviour
{
    [SerializeField] private List<TerrainState> m_TerrainStates;

    private void Update()
    {
        if (!IsHost)
        {
            print("is not host");
            return;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            print("key press");
            ChangeTerrainStateClientRpc("Volcano");
        }
    }

    [ClientRpc]
    private void ChangeTerrainStateClientRpc(string terrainState)
    {
        print("Firing");
        foreach (TerrainState state in m_TerrainStates)
        {
            if (state.name.Equals(terrainState))
            {
                HexTerrainController.Instance.LoadTerrainState(state);
                break;
            }
        }
    }
}
