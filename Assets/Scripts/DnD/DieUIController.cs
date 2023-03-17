using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieUIController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float m_RotationSpeed;
    [SerializeField] private Vector3 m_Position;

    [Header("Cache")]
    [SerializeField] private RectTransform m_RectTransform;
    private Vector3 m_StartScale;
    private Vector3 m_Rotation;

    private Coroutine m_CurrentScaleRoutine;

    private void Start()
    {
        m_StartScale = m_RectTransform.localScale;
    }

    private void Update()
    {
        m_RectTransform.eulerAngles = m_Rotation;
        m_RectTransform.position = transform.parent.position + m_Position;

        m_Rotation.y += Time.deltaTime * m_RotationSpeed;
    }

    public void DisplayUIWithAnimation(bool display)
    {
        if (m_CurrentScaleRoutine != null)
        {
            StopCoroutine(m_CurrentScaleRoutine);
        }

        m_CurrentScaleRoutine = StartCoroutine(DisplayUIRoutine(display));
    }

    public void DisplayUI(bool display)
    {
        m_RectTransform.localScale = display ? m_StartScale : Vector3.zero;
    }

    private IEnumerator DisplayUIRoutine(bool display)
    {
        Vector3 targetScale = display ? m_StartScale : Vector3.zero;
        Vector3 startScale = m_RectTransform.localScale;

        float timeElapsed = 0;
        float normalizedTime;


        do
        {
            timeElapsed += Time.deltaTime;

            normalizedTime = Mathf.Clamp01(timeElapsed / 2f);
            Vector3 newScale = Vector3.Lerp(startScale, targetScale, normalizedTime);

            m_RectTransform.localScale = newScale;

            yield return null;
        } while (normalizedTime < 1f);
    }
}
