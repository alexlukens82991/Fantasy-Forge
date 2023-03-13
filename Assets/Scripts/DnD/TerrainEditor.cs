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
    }

    private void UpdateCursor()
    {
        Ray ray = m_Cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Tile"))
            {
                m_Cursor.position = hit.point;
                m_Cursor.localScale = Vector3.one * m_CursorSize * 0.1f;
            }
        }
    }

    private void AdjustHeight()
    {
        Ray ray = m_Cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Tile"))
            {
                Collider[] hitTiles = Physics.OverlapSphere(hit.point, m_CursorSize * 0.05f);

                print(hitTiles.Length);

                foreach (Collider col in hitTiles)
                {
                    if (col.CompareTag("Tile"))
                    {
                        HexTileAnimator foundAnimator = col.gameObject.GetComponent<HexTileAnimator>();

                        foundAnimator.SetZScale(m_Height * 0.2f);
                    }
                }
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

                print(hitTiles.Length);

                foreach (Collider col in hitTiles)
                {
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
        Height
    }
}
