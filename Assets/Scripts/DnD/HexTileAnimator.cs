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
    [SerializeField] private float m_FadeActiveTime = 1;
    private Coroutine m_CurrentFadeRoutine;

    private void Start()
    {
        TileState = new(this);

        SetColor(m_StartColor);
    }

    public void SetTileActive(bool active)
    {
        if (TileState.Active != active)
        {
            TileState.Active = active;

            if (m_CurrentFadeRoutine != null)
            {
                StopCoroutine(m_CurrentFadeRoutine);
            }

            m_CurrentFadeRoutine = StartCoroutine(SetActiveRoutine(active));
        }
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

    private IEnumerator SetActiveRoutine(bool active)
    {
        float timeElapsed = 0;
        float startingAlpha = active ? 0 : 1;
        float targetAlpha = active ? 1 : 0;
        float normalizedTime;

        do
        {
            timeElapsed += Time.deltaTime;

            normalizedTime = Mathf.Clamp01(timeElapsed / m_FadeActiveTime);
            float lerpedAlpha = Mathf.Lerp(startingAlpha, targetAlpha, normalizedTime);

            Color color = m_Renderer.material.color;
            color.a = lerpedAlpha;
            m_Renderer.material.color = color;

            yield return null;
        } while (normalizedTime < 1.0f);

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
