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
    private Vector3 m_Rotation;

    private void Update()
    {
        m_RectTransform.eulerAngles = m_Rotation;
        m_RectTransform.position = transform.parent.position + m_Position;

        m_Rotation.y += Time.deltaTime * m_RotationSpeed;
    }
}
