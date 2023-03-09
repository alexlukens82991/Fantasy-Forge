using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class HexTile : NetworkBehaviour
{
    public NetworkObject CurrentModel;
    [SerializeField] private Transform m_CenterPoint;
    public KeyCode hotKey;

    public NetworkObject TestObject;
    private bool showLanding = false;
    private void Update()
    {

        if (Input.GetKeyDown(hotKey))
        {
            if (IsHost)
            {
                MoveModelToThisTileClientRpc(new NetworkObjectReference(TestObject));
            }
            else
            {
                RequestToMoveModelServerRpc(new NetworkObjectReference(TestObject));
            }

            showLanding = true;
        }
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

    private void OnDrawGizmos()
    {
        return;

        if (showLanding)
        {
            Gizmos.DrawSphere(m_CenterPoint.position, 0.1f);
        }
    }
}
