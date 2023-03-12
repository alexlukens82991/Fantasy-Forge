using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileAnimator : MonoBehaviour
{
    [SerializeField] private MeshRenderer m_Renderer;
    public Color CurrentBaseColor { get { return m_CurrentBaseColor; } private set { m_CurrentBaseColor = value; } }
    private Color m_CurrentBaseColor;

    public void SetColor(Color color)
    {
        m_Renderer.material.color = color;
        CurrentBaseColor = color;
    }
}
