using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileAnimator : MonoBehaviour
{
    [SerializeField] private MeshRenderer m_Renderer;
    public Color CurrentBaseColor { get { return m_CurrentBaseColor; } private set { m_CurrentBaseColor = value; } }
    private Color m_CurrentBaseColor;

    [Header("Status")]
    public bool Highlighted;
    public TileState TileState;

    [Header("Settings")]
    [SerializeField] private Color m_StartColor;

    private void Start()
    {
        TileState = new(this);

        SetColor(m_StartColor);
    }

    public void SetHighlighted(bool highlight)
    {
        Highlighted = highlight;

        Color color = Highlighted ? Color.magenta : CurrentBaseColor;

        m_Renderer.material.color = color;
    }

    public void SetState(bool state)
    {
        TileState.Active = state;
    }

    public void SetColor(Color color)
    {
        m_Renderer.material.color = color;
        CurrentBaseColor = color;
        TileState.Color = color;
    }

    public void SetZScale(float scale)
    {
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
        Color = hexTileAnimator.CurrentBaseColor;
        Scale = hexTileAnimator.gameObject.transform.localScale;
        Active = true;
    }
}
