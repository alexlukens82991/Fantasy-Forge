using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DungeonMaster : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsHost)
            enabled = false;

    }
}
