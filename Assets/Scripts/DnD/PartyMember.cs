using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PartyMember : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsHost && !IsOwner)
            enabled = false;
    }
}
