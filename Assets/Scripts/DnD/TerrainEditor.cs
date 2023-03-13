using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainEditor : MonoBehaviour
{
    [SerializeField] private Camera m_Cam;
    [SerializeField] private int m_CursorSize = 5;
    [SerializeField] private int m_Height = 1;
    [SerializeField] private Color m_LeftClickColor;
    [SerializeField] private Color m_RightClickColor;

    [Header("Settings")]
    [SerializeField] private TerrainMode m_TerrainMode;
    [SerializeField] private bool m_HeightLock;
    [SerializeField] private List<Color> m_PreviousColors;

    [Header("Cache")]
    [SerializeField] private Transform m_Cursor;

    private void Update()
    {
        if (m_TerrainMode == TerrainMode.Paint)
        {
            if (Input.GetMouseButton(0))
            {
                Paint(m_LeftClickColor);
            }

            if (Input.GetMouseButton(1))
            {
                Paint(m_RightClickColor);
            }
        }
        else if (m_TerrainMode == TerrainMode.Height)
        {
            if (Input.GetMouseButton(0))
            {
                AdjustHeight();
            }
        }
        if (m_TerrainMode == TerrainMode.SetActive)
        {
            if (Input.GetMouseButton(0))
            {
                SetTilesActive(true);
            }

            if (Input.GetMouseButton(1))
            {
                SetTilesActive(false);
            }
        }


        if (Input.GetKeyDown("="))
        {
            m_CursorSize++;
        }
        else if (Input.GetKeyDown("-"))
        {
            m_CursorSize--;
        }

        if (Input.GetKeyDown("2"))
        {
            m_Height++;
        }
        else if (Input.GetKeyDown("1"))
        {
            m_Height--;
        }

        UpdateCursor();
        UpdateColorList();
    }

    private void UpdateColorList()
    {
        if (m_PreviousColors.Count == 0)
        {
            m_PreviousColors.Add(m_LeftClickColor);
        }

        if (m_PreviousColors[m_PreviousColors.Count - 1] != m_LeftClickColor)
        {
            m_PreviousColors.Add(m_LeftClickColor);
        }
    }

    private void UpdateCursor()
    {
        Ray ray = m_Cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Tile"))
            {
                Vector3 position = hit.point;
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    position = new(hit.point.x, hit.point.y, m_Cursor.transform.position.z);
                    
                }
                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    position = new(m_Cursor.transform.position.x, hit.point.y,hit.point.z);
                }

                m_Cursor.position = position;
                m_Cursor.localScale = Vector3.one * m_CursorSize * 0.1f;
            }
        }
    }

    private void SetTilesActive(bool active)
    {
        Collider[] hitTiles = Physics.OverlapSphere(m_Cursor.transform.position, m_CursorSize * 0.05f);

        foreach (Collider col in hitTiles)
        {
            if (col.CompareTag("Tile"))
            {
                HexTileAnimator foundAnimator = col.gameObject.GetComponent<HexTileAnimator>();

                foundAnimator.SetHighlighted(active);
            }
        }
    }

    private void AdjustHeight()
    {
        Collider[] hitTiles = Physics.OverlapSphere(m_Cursor.transform.position, m_CursorSize * 0.05f);

        foreach (Collider col in hitTiles)
        {
            if (col.CompareTag("Tile"))
            {
                HexTileAnimator foundAnimator = col.gameObject.GetComponent<HexTileAnimator>();

                foundAnimator.SetZScale(m_Height * 0.2f);
            }
        }
    }

    private void Paint(Color color)
    {
        Ray ray = m_Cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Tile"))
            {
                Collider[] hitTiles = Physics.OverlapSphere(hit.point, m_CursorSize * 0.05f);

                foreach (Collider col in hitTiles)
                {
                    if (m_HeightLock)
                    {
                        if (hit.collider.transform.localScale.z != col.transform.localScale.z)
                        {
                            continue;
                        }
                    }
                    if (col.CompareTag("Tile"))
                    {
                        HexTileAnimator foundAnimator = col.gameObject.GetComponent<HexTileAnimator>();

                        if (!color.Equals(foundAnimator.CurrentBaseColor))
                        {
                            foundAnimator.SetColor(color);
                        }
                    }
                }
            }
        }
    }

    public enum TerrainMode
    {
        None,
        Paint,
        Height,
        SetActive
    }
}
