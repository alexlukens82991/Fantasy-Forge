using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileAnimator : MonoBehaviour
{
    [Header("Cache")]
    public HexTile HexTile;
    [SerializeField] private MeshRenderer m_Renderer;

    

    [Header("Settings")]
    [SerializeField] private Color m_StartColor;
    [SerializeField] private float m_StateTransitionTime = 4;
    private Coroutine m_CurrentScaleRoutine;
    private Coroutine m_CurrentColorRoutine;

    private void Start()
    {
        SetColor(m_StartColor);
    }

    public void SetTileState(TileState tileState, bool useAnimation = true)
    {
        SetTileActive(tileState.Active);

        if (tileState.Active)
        {
            if (useAnimation)
            {
                LerpColor(tileState.Color);
                LerpScale(tileState.Scale);
            }
            else
            {
                SetColor(tileState.Color);
                SetScale(tileState.Scale);
            }
        }
    }

    public void SetTileActive(bool active)
    {
        if (HexTile.TileState.Active != active)
        {
            HexTile.TileState.Active = active;

            m_Renderer.enabled = active;

            //if (m_CurrentFadeRoutine != null)
            //{
            //    StopCoroutine(m_CurrentFadeRoutine);
            //}

            //m_CurrentFadeRoutine = StartCoroutine(SetActiveRoutine(active));
        }
    }

    public void SetColor(Color color)
    {
        if (!HexTile.TileState.Active)
            return;
        
        m_Renderer.sharedMaterial.color = color;
        HexTile.TileState.Color = color;
    }

    public void LerpColor(Color color)
    {
        if (m_CurrentColorRoutine != null)
        {
            StopCoroutine(m_CurrentColorRoutine);
        }

        m_CurrentColorRoutine = StartCoroutine(LerpColorRoutine(color));
    }

    public void SetZScale(float scale)
    {
        if (!HexTile.TileState.Active)
            return;

        Vector3 newScale = new Vector3(transform.localScale.x, transform.localScale.y, scale);

        transform.localScale = newScale;
        HexTile.TileState.Scale = transform.localScale;
    }

    public void SetScale(Vector3 scale)
    {
        if (!HexTile.TileState.Active)
            return;

        transform.localScale = scale;
    }

    public void LerpScale(Vector3 scale)
    {
        if (m_CurrentScaleRoutine != null)
        {
            StopCoroutine(m_CurrentScaleRoutine);
        }

        m_CurrentScaleRoutine = StartCoroutine(LerpScaleRoutine(scale));
    }

    public IEnumerator LerpScaleRoutine(Vector3 scale)
    {
        float timeElapsed = 0;
        Vector3 start = transform.localScale;
        Vector3 target = scale;
        float normalizedTime;

        do
        {
            timeElapsed += Time.deltaTime;

            normalizedTime = Mathf.Clamp01(timeElapsed / m_StateTransitionTime);
            Vector3 newScale = Vector3.Lerp(start, target, normalizedTime);

            transform.localScale = newScale;

            yield return null;
        } while (normalizedTime < 1f);

        HexTile.TileState.Scale = scale;

    }

    private IEnumerator LerpColorRoutine(Color color)
    {
        float timeElapsed = 0;
        Color start = m_Renderer.material.color;
        Color target = color;
        float normalizedTime;

        do
        {
            timeElapsed += Time.deltaTime;

            normalizedTime = Mathf.Clamp01(timeElapsed / m_StateTransitionTime);
            Color newColor = Color.Lerp(start, target, normalizedTime);

            m_Renderer.material.color = newColor;
            yield return null;
        } while (normalizedTime < 1f);

        HexTile.TileState.Color = color;
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

            normalizedTime = Mathf.Clamp01(timeElapsed / m_StateTransitionTime);
            float lerpedAlpha = Mathf.Lerp(startingAlpha, targetAlpha, normalizedTime);

            Color color = m_Renderer.material.color;
            color.a = lerpedAlpha;
            m_Renderer.material.color = color;

            yield return null;
        } while (normalizedTime < 1f);

        HexTile.TileState.Active = active;


    }
}

