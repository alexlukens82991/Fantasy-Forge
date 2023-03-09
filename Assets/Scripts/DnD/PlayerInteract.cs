using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerInteract : NetworkBehaviour
{
    [SerializeField] private Camera playerCam;
    private Animator currentActiveTileAnimator;
    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
            enabled = false;

        foreach (KeyValuePair<ulong, NetworkObject> item in NetworkManager.Singleton.SpawnManager.SpawnedObjects)
        {
            print(item.Value.name);
        }

    }

    private void Update()
    {
        UpdateHoverTile();
    }

    private void UpdateHoverTile()
    {
        Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 5))
        {
            if (hit.collider.tag.Equals("Tile"))
            {
                if (currentActiveTileAnimator)
                {
                    if (hit.collider.gameObject == currentActiveTileAnimator.gameObject)
                        return;
                }

                Animator foundAnimator = hit.collider.GetComponent<Animator>();

                if (foundAnimator)
                {
                    NetworkObject netObj;
                    NetworkObjectReference objRef;

                    if (currentActiveTileAnimator)
                    {
                        netObj = currentActiveTileAnimator.GetComponent<NetworkObject>();
                        objRef = new NetworkObjectReference(netObj);

                        if (IsHost)
                            UpdateAnimatorClientRpc(objRef, false);
                        else
                            RequestToUpdateServerAnimatorsServerRpc(objRef, false);
                    }

                    currentActiveTileAnimator = foundAnimator;

                    netObj = currentActiveTileAnimator.GetComponent<NetworkObject>();
                    objRef = new NetworkObjectReference(netObj);

                    if (IsHost)
                        UpdateAnimatorClientRpc(objRef, true);
                    else
                        RequestToUpdateServerAnimatorsServerRpc(objRef, true);

                }
            }
        }
    }

    [ServerRpc]
    private void RequestToUpdateServerAnimatorsServerRpc(NetworkObjectReference netObj, bool state)
    {
        UpdateAnimatorClientRpc(netObj, state);
    }

    [ClientRpc]
    private void UpdateAnimatorClientRpc(NetworkObjectReference netObj, bool state)
    {
        print("updating clients");
        if(netObj.TryGet(out NetworkObject foundAnimator))
        {
            Animator animator = foundAnimator.GetComponent<Animator>();

            animator.SetBool("Active", state);
        }
        else
        {
            Debug.LogError("COULD NOT FIND NET OBJECT");
        }
        
    }
}
