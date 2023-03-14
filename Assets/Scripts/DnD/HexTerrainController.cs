using LukensUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HexTerrainController : Singleton<HexTerrainController>
{
    public TerrainState CurrentTerrainState;
    public TerrainState[] m_TestableStates;
    private int currState;
    public List<HexTile> HexTiles;
    [SerializeField] private Transform m_BuildingsParent;
    [SerializeField] private GameObject m_GenericBuildingPrefab;

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

    private IEnumerator ActivateBuildingsRoutine(TerrainState terrainState)
    {
        yield return new WaitForSeconds(3);

        foreach (BuildingState buildingState in terrainState.BuildingStates)
        {
            StartCoroutine(SpawnBuilding(buildingState));
        }
    }

    private IEnumerator SpawnBuilding(BuildingState buildingState)
    {
        GameObject newBuilding = Instantiate(m_GenericBuildingPrefab, m_BuildingsParent);
        newBuilding.transform.localScale = Vector3.zero;
        newBuilding.transform.position = buildingState.Position;
        newBuilding.transform.rotation = buildingState.Rotation;

        float timeElapsed = 0;
        Vector3 start = Vector3.zero;
        Vector3 target = buildingState.Scale;
        float normalizedTime;

        yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f, 4.0f));

        do
        {
            timeElapsed += Time.deltaTime;

            normalizedTime = Mathf.Clamp01(timeElapsed / 2f);
            Vector3 newScale = Vector3.Lerp(start, target, normalizedTime);

            newBuilding.transform.localScale = newScale;

            yield return null;
        } while (normalizedTime < 1f);
    }

    public void LoadTerrainState(TerrainState terrainState)
    {
        foreach (HexTile hexTile in HexTiles)
        {
            string[] expanded = hexTile.gameObject.name.Split('_');
            hexTile.SetTileState(terrainState.TileStates[int.Parse(expanded[1])]);
        }

        foreach (Transform child in m_BuildingsParent)
        {
            Destroy(child.gameObject);
        }

        StartCoroutine(ActivateBuildingsRoutine(terrainState));
    }

    
}

[Serializable]
public class BuildingState
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale;

    public BuildingState(Transform buildingTransform)
    {
        Position = buildingTransform.position;
        Rotation = buildingTransform.rotation;
        Scale = buildingTransform.localScale;
    }
}
