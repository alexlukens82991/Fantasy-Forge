using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileAnimator : MonoBehaviour
{
    [SerializeField] private MeshRenderer m_Renderer;

    [Header("Status")]
    public TileState TileState;

    [Header("Settings")]
    [SerializeField] private Color m_StartColor;

    private void Start()
    {
        TileState = new(this);

        SetColor(m_StartColor);
    }

    public void SetTileActive(bool active)
    {
        TileState.Active = active;

        Color color = active ? TileState.Color : Color.magenta;

        m_Renderer.material.color = color;

    }

    public void SetColor(Color color)
    {
        if (!TileState.Active)
            return;
        
        m_Renderer.material.color = color;
        TileState.Color = color;
        TileState.Color = color;
    }

    public void SetZScale(float scale)
    {
        if (!TileState.Active)
            return;

        Vector3 newScale = new Vector3(transform.localScale.x, transform.localScale.y, scale);

        transform.localScale = newScale;
        TileState.Scale = transform.localScale;
    }
}

public class TileState
{
    public Color Color;
    public Vector3 Scale;
    public bool Active;

    public TileState(HexTileAnimator hexTileAnimator)
    {
        Color = Color.grey;
        Scale = hexTileAnimator.gameObject.transform.localScale;
        Active = true;
    }
}
