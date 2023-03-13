using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class HexTile : NetworkBehaviour
{
    public NetworkObject CurrentModel;
    [SerializeField] private Transform m_CenterPoint;

    [Header("Status")]
    public TileState TileState;

    [Header("Cache")]
    public HexTileAnimator HexTileAnimator;

    //        if (IsHost)
    //        {
    //            MoveModelToThisTileClientRpc(new NetworkObjectReference(TestObject));
    //        }
    //        else
    //        {
    //            RequestToMoveModelServerRpc(new NetworkObjectReference(TestObject));
    //        }


    private void Awake()
    {
        TileState = new(this);
    }

    public void SetTileState(TileState tileState)
    {
        HexTileAnimator.SetTileState(tileState);
    }

    [ServerRpc(RequireOwnership = false)]
    public void RequestToMoveModelServerRpc(NetworkObjectReference modelObject)
    {
        MoveModelToThisTileClientRpc(modelObject);
    }

    [ClientRpc]
    public void MoveModelToThisTileClientRpc(NetworkObjectReference modelObject)
    {
        if (modelObject.TryGet(out NetworkObject foundModel))
        {
            foundModel.transform.position = m_CenterPoint.position;
        }
        else
        {
            Debug.LogError("COULD NOT FIND NET OBJECT");
        }
    }
}

[Serializable]
public class TileState
{
    // TODO: Need custom saving for the object.
    // object will need to be abstract class that can be saved
    public Color Color;
    public Vector3 Scale;
    public bool Active;

    public TileState(HexTile hexTile)
    {
        Color = Color.grey;
        Scale = hexTile.transform.localScale;
        Active = true;
    }
}
